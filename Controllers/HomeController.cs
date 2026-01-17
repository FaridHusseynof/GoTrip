using GoTrip.Data;
using GoTrip.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoTrip.Controllers
{
    public class HomeController : Controller
    {
        private TripDbContext _context { get; }
        public HomeController(TripDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.tours.Where(c=>!c.IsDeleted));
        }

    }
}
