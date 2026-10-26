
using MediCart.Data;
using MediCart.DTOs;
using MediCart.Models;
using MediCart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCart.Controllers;

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

    // GET: /Medicine/Index?search=...
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

    // GET: /Medicine/Details/1
    public async Task<IActionResult> Details(int id)
    {
        var medicine = await _db.Medicines
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (medicine == null)
            return NotFound();

        return View(medicine);
    }

    // GET: /Medicine/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(new MedicineForm());
    }

    // POST: /Medicine/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        MedicineForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var medicine = new Medicine
        {
            Name = form.Name,
            Manufacturer = form.Manufacturer,
            Price = form.Price,
            StockQuantity = form.StockQuantity,
            ExpiryDate = form.ExpiryDate
        };

        _db.Medicines.Add(medicine);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Created medicine {MedicineId}",
            medicine.Id);

        TempData["Success"] = "Medicine created.";

        return RedirectToAction(nameof(Index));
    }

    // GET: /Medicine/Edit/1
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var medicine = await _db.Medicines
            .FindAsync(id);

        if (medicine == null)
            return NotFound();

        return View(new MedicineForm
        {
            Name = medicine.Name,
            Manufacturer = medicine.Manufacturer,
            Price = medicine.Price,
            StockQuantity = medicine.StockQuantity,
            ExpiryDate = medicine.ExpiryDate
        });
    }

    // POST: /Medicine/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id, MedicineForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var medicine = await _db.Medicines.FindAsync(id);

        if (medicine == null)
            return NotFound();

        medicine.Name = form.Name;
        medicine.Manufacturer = form.Manufacturer;
        medicine.Price = form.Price;
        medicine.StockQuantity = form.StockQuantity;
        medicine.ExpiryDate = form.ExpiryDate;

        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Updated medicine {MedicineId}", id);

        TempData["Success"] = "Medicine updated.";

        return RedirectToAction(nameof(Index));
    }

    // POST: /Medicine/Delete/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var medicine = await _db.Medicines.FindAsync(id);

        if (medicine == null)
            return NotFound();

        _db.Medicines.Remove(medicine);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Deleted medicine {MedicineId}", id);

        TempData["Success"] = "Medicine deleted.";

        return RedirectToAction(nameof(Index));
    }
}
