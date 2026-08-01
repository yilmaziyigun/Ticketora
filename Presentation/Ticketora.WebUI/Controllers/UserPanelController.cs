using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Persistence.Identity;
using Ticketora.WebUI.Models;

namespace Ticketora.WebUI.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public UserPanelController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var tickets = await _mediator.Send(new GetUserTicketsQuery(user.Id));
            var now = DateTime.Now;

            var model = new UserPanelViewModel
            {
                Name = user.Name ?? string.Empty,
                Surname = user.Surname ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                ActiveTickets = tickets.Where(x => x.EventDate >= now && !x.IsUsed).ToList(),
                PastTickets = tickets.Where(x => x.EventDate < now || x.IsUsed).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            return View(new ProfileViewModel
            {
                Name = user.Name ?? string.Empty,
                Surname = user.Surname ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty
            });
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["ProfileMessage"] = "Profil bilgileriniz güncellendi.";
                return RedirectToAction(nameof(Profile));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
