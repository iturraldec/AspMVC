using Microsoft.AspNetCore.Mvc;
using AspMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspMVC.Controllers;

public class HomeController : Controller
{
    private NorthwindContext _context;
    public HomeController(NorthwindContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        IEnumerable<SelectListItem> items = from value in _context.Categories.OrderBy(v => v.CategoryName).ToList()
                                            select new SelectListItem(value.CategoryName, value.CategoryId.ToString());
        
        return View(items);
    }

    public ActionResult GetProductos(int? idCategoria)
    {
        var products = _context.Products
                                .Where(p => p.CategoryId == idCategoria)
                                .OrderBy(p => p.ProductName)
                                .ToList();

        return Ok(products);
    }
    [HttpPost]
    public ActionResult AddProduct([FromBody] ProductRequest productRequest)
    {
        var product  = new Product();

        product.CategoryId = productRequest.CategoryId;
        product.ProductName = productRequest.Name;
        product.QuantityPerUnit = productRequest.Unit;
        product.UnitPrice = productRequest.Price;
        _context.Products.Add(product);
        _context.SaveChanges();
        return Ok(product);
    }
}
