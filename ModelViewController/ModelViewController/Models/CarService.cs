using System;
using System.Text.Json;

namespace ModelViewController.Models;

public class CarService
{
    private readonly IHttpContextAccessor accessor;

    public CarService(IHttpContextAccessor accessor)
    {
        this.accessor= accessor;
    }
    public void Add(Product product)
    {
        List<Product> data = List();
        data.Add(product);
        HttpContext ctx = accessor.HttpContext;
        ctx.Session.SetString("cart", JsonSerializer.Serialize(data));
    }

    public List<Product> List()
    {
        HttpContext ctx = accessor.HttpContext;
        string json = ctx.Session.GetString("cart") ?? "[]";
        return JsonSerializer.Deserialize<List<Product>>(json);
    }
}
