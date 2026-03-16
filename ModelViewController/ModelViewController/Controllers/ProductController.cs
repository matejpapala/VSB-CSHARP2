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

    public IActionResult Detail()
    {
        return View();
    }
}
