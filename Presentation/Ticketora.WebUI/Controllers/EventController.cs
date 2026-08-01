using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketora.Application.Features.CQRSDesignPattern.Categories.Handlers;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Commands;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Handlers;
using Ticketora.Application.Features.CQRSDesignPattern.Event.Queries;
using Ticketora.WebUI.Constants;

namespace Ticketora.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly GetEventQueryHandler _getEventQueryHandler;
        private readonly GetByIdQueryHandler _getByIdQueryHandler;
        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
        private readonly CreateEventCommandHandler _createEventCommandHandler;
        private readonly UpdateEventCommandHandler _updateEventCommandHandler;
        private readonly RemoveEventCommandHandler _removeEventCommandHandler;

        public EventController(
            GetEventQueryHandler getEventQueryHandler,
            GetByIdQueryHandler getByIdQueryHandler,
            GetCategoryQueryHandler getCategoryQueryHandler,
            CreateEventCommandHandler createEventCommandHandler,
            UpdateEventCommandHandler updateEventCommandHandler,
            RemoveEventCommandHandler removeEventCommandHandler)
        {
            _getEventQueryHandler = getEventQueryHandler;
            _getByIdQueryHandler = getByIdQueryHandler;
            _getCategoryQueryHandler = getCategoryQueryHandler;
            _createEventCommandHandler = createEventCommandHandler;
            _updateEventCommandHandler = updateEventCommandHandler;
            _removeEventCommandHandler = removeEventCommandHandler;
        }

        public async Task<IActionResult> EventList(string? search, int? categoryId, string? location, string? dateFilter)
        {
            var allEvents = await _getEventQueryHandler.Handle();
            var values = allEvents;
            var now = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(search))
            {
                values = values
                    .Where(x => x.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                        || x.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                values = values.Where(x => x.CategoryId == categoryId.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                values = values
                    .Where(x => x.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            values = dateFilter switch
            {
                "today" => values.Where(x => x.EventDate.Date == now.Date).ToList(),
                "week" => values.Where(x => x.EventDate.Date >= now.Date && x.EventDate.Date <= now.Date.AddDays(7)).ToList(),
                "month" => values.Where(x => x.EventDate.Date >= now.Date && x.EventDate.Date <= now.Date.AddMonths(1)).ToList(),
                "past" => values.Where(x => x.EventDate < now).ToList(),
                _ => values.Where(x => x.EventDate >= now && x.Status).ToList()
            };

            ViewBag.Categories = await _getCategoryQueryHandler.Handle();
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Location = location;
            ViewBag.DateFilter = dateFilter;
            ViewBag.Locations = allEvents.Select(x => x.Location).Distinct().OrderBy(x => x).ToList();

            return View(values);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var value = await _getByIdQueryHandler.Handle(new GetByIdEventQuery
            {
                Id = id
            });

            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            ViewBag.Categories = await _getCategoryQueryHandler.Handle();
            return View();
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventCommand command)
        {
            command.Status = true;
            await _createEventCommandHandler.Handle(command);
            return RedirectToAction("Events", "Admin");
        }

        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _removeEventCommandHandler.Handle(new RemoveEventCommand
            {
                Id = id
            });
            return RedirectToAction("Events", "Admin");
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int id)
        {
            var value = await _getByIdQueryHandler.Handle(new GetByIdEventQuery
            {
                Id = id
            });
            return View(value);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(UpdateEventCommand command)
        {
            await _updateEventCommandHandler.Handle(command);
            return RedirectToAction("Events", "Admin");
        }
    }
}
