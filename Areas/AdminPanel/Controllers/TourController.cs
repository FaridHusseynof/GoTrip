using GoTrip.Areas.AdminPanel.ViewModels;
using GoTrip.Data;
using GoTrip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoTrip.Areas.AdminPanel.Controllers
{
    [Authorize(Roles ="Admin, SuperAdmin")]
    [Area("AdminPanel")]
    public class TourController : Controller
    {
        private TripDbContext _context { get; }
        public TourController(TripDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.tours.Where(c => !c.IsDeleted));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Tour tour = new Tour
            {
                Title=vm.title,
                Price=vm.price,
                Rating=vm.rating,
                IsDeleted=false
            };
            if (tour==null) return NotFound();
            if (vm.imageFile!=null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                tour.ImageURL=fileName;
            }
            _context.Add(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null) return BadRequest();
            Tour? tour = _context.tours.Where(c => !c.IsDeleted).FirstOrDefault(i=>i.Id==id);
            if (tour==null) return NotFound();
            tour.IsDeleted=true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id) 
        {
            if (id==null) return BadRequest();
            Tour? tour = _context.tours.Where(c => !c.IsDeleted).FirstOrDefault(i => i.Id==id);
            if (tour==null) return NotFound();
            UpdateVM vm = new UpdateVM
            {
                title=tour.Title,
                price=tour.Price,
                rating=tour.Rating,
                id_=tour.Id
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateVM vm) 
        {
            if (!ModelState.IsValid) return View(vm);
            Tour? tour = _context.tours.Where(c => !c.IsDeleted).FirstOrDefault(i => i.Id==vm.id_);
            if (tour==null) return NotFound();
            tour.Title=vm.title;
            tour.Price=vm.price;
            tour.Rating=vm.rating;
            if (vm.imageFile!=null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                tour.ImageURL=fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
