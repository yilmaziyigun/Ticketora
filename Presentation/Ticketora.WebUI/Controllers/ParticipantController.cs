using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Commands;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries;
using Ticketora.Persistence.Identity;
using Ticketora.WebUI.Constants;

namespace Ticketora.WebUI.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class ParticipantController : Controller
    {
        private readonly IMediator _mediator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParticipantController(
            IMediator mediator,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> ParticipantList()
        {
            var values = await _mediator.Send(new GetParticipantsQuery());
            return View(values);
        }

        public IActionResult CreateParticipant()
        {
            SetRoleOptions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant(CreateParticipantsCommand command)
        {
            command.CheckInDate = DateTime.Now;
            SetRoleOptions(command.Role);

            if (string.IsNullOrWhiteSpace(command.Role))
            {
                ModelState.AddModelError(nameof(command.Role), "Rol seçiniz.");
                return View(command);
            }

            if (!await _roleManager.RoleExistsAsync(command.Role))
            {
                ModelState.AddModelError(nameof(command.Role), "Seçilen rol bulunamadı.");
                return View(command);
            }

            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user == null)
            {
                ModelState.AddModelError(nameof(command.Email), "Bu e-posta ile kayıtlı üye bulunamadı.");
                return View(command);
            }

            if (!await _userManager.IsInRoleAsync(user, command.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, command.Role);
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(command);
                }
            }

            await _mediator.Send(command);
            return RedirectToAction("ParticipantList");
        }

        public async Task<IActionResult> ParticipantDetails(int id)
        {
            var values = await _mediator.Send(new GetByIdParticipantsQuery(id));
            return View(values);
        }

        public async Task<IActionResult> DeleteParticipant(int id)
        {
            await _mediator.Send(new RemoveParticipantsCommand(id));
            return RedirectToAction("ParticipantList");
        }

        private void SetRoleOptions(string? selectedRole = null)
        {
            ViewBag.Roles = _roleManager.Roles
                .Select(x => x.Name)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .OrderBy(x => x)
                .ToList();
            ViewBag.SelectedRole = selectedRole;
        }
    }
}
