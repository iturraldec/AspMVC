using Microsoft.AspNetCore.Mvc;
using AspMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspMVC.Controllers;

public class HomeController : Controller
{
    private NorthwindContext _context;
    public HomeController(NorthwindContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        IEnumerable<SelectListItem> items = from value in _context.Categories.OrderBy(v => v.CategoryName).ToList()
                                            select new SelectListItem(value.CategoryName, value.CategoryId.ToString());
        
        return View(items);
    }

    public async Task<ActionResult> GetProductos(int? idCategoria)
    {
        var products = await _context.Products
                                .Where(p => p.CategoryId == idCategoria)
                                .OrderBy(p => p.ProductName)
                                .ToListAsync();

        return Ok(products);
    }
}
