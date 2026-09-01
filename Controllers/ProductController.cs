using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTO;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductController(AppDbContext dbContext) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await dbContext.Products.ToListAsync();
            return View(products);
        }

       
        public async Task<IActionResult> Create(AddProductDTO dto)
        {
            if(dto is null || string.IsNullOrEmpty(dto.Name)
                || string.IsNullOrEmpty(dto.Description) || float.IsNegative(dto.Price))
            {
                TempData["ErrorMsg"] = "Please fill out details..";
                ViewBag.ErrorMsg = "Please fill out details..";
                return RedirectToAction("Index","Product");
            }
            var data = new Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            dbContext.Products.Add(data);
            await dbContext.SaveChangesAsync();

            TempData["SuccessMsg"] = "Product Added Successfully!!";
            return RedirectToAction("Index", "Product");

        }
    }
}
