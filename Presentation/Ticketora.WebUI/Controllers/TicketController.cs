using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Queries;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Commands;
using Ticketora.Application.Features.MediatorDesignPattern.Tickets.Queries;
using Ticketora.Persistence.Identity;

namespace Ticketora.WebUI.Controllers
{
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;
        private readonly GetByIdQueryHandler _getByIdQueryHandler;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(
            IMediator mediator,
            GetByIdQueryHandler getByIdQueryHandler,
            UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _getByIdQueryHandler = getByIdQueryHandler;
            _userManager = userManager;
        }

        public async Task<IActionResult> TicketList()
        {
            var values = await _mediator.Send(new GetTicketQuery());
            return View(values);
        }

        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("TicketList");
        }

        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _mediator.Send(new RemoveTicketCommand(id));
            return RedirectToAction("TicketList");
        }

        public async Task<IActionResult> UpdateTicket(int id)
        {
            var values = await _mediator.Send(new GetByIdTicketQuery(id));
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTicket(UpdateTicketCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("TicketList");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Book(int eventId)
        {
            var values = await _getByIdQueryHandler.Handle(new GetByIdEventQuery
            {
                Id = eventId
            });

            if (values == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            ViewBag.EventTitle = values.Title;
            ViewBag.EventDate = values.EventDate;
            ViewBag.Location = values.Location;
            ViewBag.Price = values.Price;

            return View(new BookTicketCommand
            {
                EventId = values.EventId,
                UserId = user.Id,
                Name = user.Name ?? string.Empty,
                Surname = user.Surname ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Book(BookTicketCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            command.UserId = user.Id;
            command.Email = string.IsNullOrWhiteSpace(command.Email) ? user.Email ?? string.Empty : command.Email;
            command.Name = string.IsNullOrWhiteSpace(command.Name) ? user.Name ?? string.Empty : command.Name;
            command.Surname = string.IsNullOrWhiteSpace(command.Surname) ? user.Surname ?? string.Empty : command.Surname;
            command.PhoneNumber = string.IsNullOrWhiteSpace(command.PhoneNumber) ? user.PhoneNumber ?? string.Empty : command.PhoneNumber;

            var ticketId = await _mediator.Send(command);

            return RedirectToAction("Detail", new { id = ticketId });
        }

        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            var ticket = await _mediator.Send(new GetByIdTicketQuery(id));
            if (ticket == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (ticket.UserId != userId)
            {
                return Forbid();
            }

            return View(ticket);
        }
    }
}
