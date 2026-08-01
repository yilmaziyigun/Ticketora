using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Ticketora.Persistence.Context;
using Ticketora.WebUI.Models;

namespace Ticketora.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TicketoraContext _context;

        public HomeController(ILogger<HomeController> logger, TicketoraContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;
            var upcomingEvents = await _context.Events.CountAsync(x => x.Status && x.EventDate >= now);
            var cityCount = await _context.Events
                .Where(x => x.Status && x.EventDate >= now)
                .Select(x => x.Location)
                .Distinct()
                .CountAsync();
            var concertEvents = await (
                from eventItem in _context.Events
                join category in _context.Categories on eventItem.CategoryId equals category.CategoryId
                where EF.Functions.Like(category.CategoryName, "%Konser%")
                select eventItem).CountAsync();
            var cinemaEvents = await (
                from eventItem in _context.Events
                join category in _context.Categories on eventItem.CategoryId equals category.CategoryId
                where EF.Functions.Like(category.CategoryName, "%Sinema%")
                select eventItem).CountAsync();

            ViewBag.UpcomingEvents = upcomingEvents;
            ViewBag.CityCount = cityCount;
            ViewBag.ConcertEvents = concertEvents;
            ViewBag.CinemaEvents = cinemaEvents;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
