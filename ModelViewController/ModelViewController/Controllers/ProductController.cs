using System;
using Microsoft.AspNetCore.Mvc;
using ModelViewController.Models;

namespace ModelViewController.Controllers;

public class ProductController : Controller
{
    public IActionResult Index(ProductService productService)
    {
        ViewBag.Products = productService.List();
        return View();
    }

    public IActionResult Detail(int id, AddToCartForm form, [FromServices]ProductService productService)
    {
        Product product = productService.GetProductById(id);
        ViewBag.Product = product;
        return View(new AddToCartForm()
        {
            Id = id,
            Quantity = 1
        });
    }

    [HttpPost]
    public IActionResult Detail(int id,AddToCartForm form, [FromServices]ProductService productService, [FromServices]CarService carService)
    {
        Product product = productService.GetProductById(id);
        if(ModelState.IsValid)
        {
            if(form.Quantity == 2)
            {
                ModelState.AddModelError("Quantity", "Nesmi byt 2");    
            }
            if(ModelState.IsValid)
            {
                for(int i = 0;i < form.Quantity;i++)
                {
                    carService.Add(product);    
                }
                return RedirectToAction("Cart");
            }
        }
        ViewBag.Product = product;
        return View(form);
    }

    public IActionResult Cart([FromServices]CarService carService)
    {
        ViewBag.Items = carService.List();
        return View();
    }
}
