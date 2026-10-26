using MediCart.Data;
using MediCart.DTOs;
using MediCart.Models;
using MediCart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCart.Controllers
{
    public class MedicineController : Controller
    {
        private readonly MedicineDbContext _db;
        private readonly IShopMessageService _messageService;
        private readonly ILogger<MedicineController> _logger;

        public MedicineController(
            MedicineDbContext db,
            IShopMessageService messageService,
            ILogger<MedicineController> logger)
        {
            _db = db;
            _messageService = messageService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string? search)
        {
            IQueryable<Medicine> query =
                _db.Medicines.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m =>
                    m.Name.Contains(search));
            }

            var medicines = await query
                .OrderBy(m => m.Name)
                .ToListAsync();

            ViewBag.Welcome = _messageService.GetWelcomeMessage();
            ViewBag.Search = search;

            return View(medicines);
        }


    }
}
