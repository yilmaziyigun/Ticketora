using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Commands;
using Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries;

namespace Ticketora.WebUI.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IMediator _mediator;

        public ParticipantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> ParticipantList()
        {
            var values = await _mediator.Send(new GetParticipantsQuery());
            return View(values);
        }
        public IActionResult CreateParticipant()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateParticipant(CreateParticipantsCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("ParticipantList");
        }
        public async Task<IActionResult> ParticipantDetails(int id)
        {
            var values = await _mediator.Send(new Ticketora.Application.Features.MediatorDesignPattern.Participants.Queries.GetByIdParticipantsQuery(id));
            return View(values);
        }
    }
}
