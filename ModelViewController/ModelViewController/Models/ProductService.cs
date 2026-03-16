using System;

namespace ModelViewController.Models;

public class ProductService
{
    public List<Product> List()
    {
        return Product.GetProducts();
    }

    public Product GetProductById(int id)
    {
        return Product.GetProducts().FirstOrDefault(x => x.Id == id);
    } 
}
