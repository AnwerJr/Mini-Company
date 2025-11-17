using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Models;

public class AccessoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccessoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Helmets()
    {
        var helmets = _context.Helmet.ToList();
        return View(helmets);
    }
}
