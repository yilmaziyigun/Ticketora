using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Commands;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Queries;

namespace Ticketora.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly GetEventQueryHandler _getEventQueryHandler;
        private readonly GetByIdQueryHandler _getByIdQueryHandler;
        private readonly CreateEventCommandHandler _createEventCommandHandler;
        private readonly UpdateEventCommandHandler _updateEventCommandHandler;
        private readonly RemoveEventCommandHandler _removeEventCommandHandler;

        public EventController(GetEventQueryHandler getEventQueryHandler, GetByIdQueryHandler getByIdQueryHandler, CreateEventCommandHandler createEventCommandHandler, UpdateEventCommandHandler updateEventCommandHandler, RemoveEventCommandHandler removeEventCommandHandler)
        {
            _getEventQueryHandler = getEventQueryHandler;
            _getByIdQueryHandler = getByIdQueryHandler;
            _createEventCommandHandler = createEventCommandHandler;
            _updateEventCommandHandler = updateEventCommandHandler;
            _removeEventCommandHandler = removeEventCommandHandler;
        }

        public async Task<IActionResult> EventList()
        {
            var values = await _getEventQueryHandler.Handle();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventCommand command)
        {
            command.Status = true;
            await _createEventCommandHandler.Handle(command);
            return RedirectToAction("EventList");
        }
        public async Task<IActionResult> DeleteEvent(int id)
        {
            return RedirectToAction("EventList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int id)
        {
            var value = await _getByIdQueryHandler.Handle(new GetByIdEventQuery());
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(UpdateEventCommand command)
        {
            await _updateEventCommandHandler.Handle(command);
            return RedirectToAction("EventList");
        }
    }
}
