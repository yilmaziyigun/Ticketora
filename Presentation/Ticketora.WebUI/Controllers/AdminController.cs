using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries;
using Ticketora.WebUI.Constants;

namespace Ticketora.WebUI.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly GetEventQueryHandler _getEventQueryHandler;
        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
        private readonly IMediator _mediator;

        public AdminController(GetEventQueryHandler getEventQueryHandler, GetCategoryQueryHandler getCategoryQueryHandler, IMediator mediator)
        {
            _getEventQueryHandler = getEventQueryHandler;
            _getCategoryQueryHandler = getCategoryQueryHandler;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _getEventQueryHandler.Handle();
            var categories = await _getCategoryQueryHandler.Handle();
            var participants = await _mediator.Send(new GetParticipantsQuery());

            ViewBag.EventCount = events.Count;
            ViewBag.CategoryCount = categories.Count;
            ViewBag.ParticipantCount = participants.Count;
            ViewBag.ActiveEventCount = events.Count(x => x.EventDate >= DateTime.Now && x.Status);

            return View();
        }

        public async Task<IActionResult> Events()
        {
            var values = await _getEventQueryHandler.Handle();
            return View(values);
        }

        public async Task<IActionResult> Categories()
        {
            var values = await _getCategoryQueryHandler.Handle();
            return View(values);
        }

        public async Task<IActionResult> Participants()
        {
            var values = await _mediator.Send(new GetParticipantsQuery());
            return View(values);
        }
    }
}
