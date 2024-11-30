using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

class ProductsWithSameTypeVisitor : IProductVisitor
{
    public void Visit(Clothing clothing)
    {
        PrintAllProduct(nameof(Clothing));
    }

    public void Visit(Toy toy)
    {
        PrintAllProduct(nameof(Toy));
    }

    public void Visit(Grocery grocery)
    {
        PrintAllProduct(nameof(Grocery));
    }

    private void PrintAllProduct(string productType)
    {
        var AllProducts = Database.Products.FindAll(item => item.Catagory == productType);
        Console.WriteLine($"------------------- {productType} -------------------");
        foreach (var item in AllProducts)
        {
            Console.WriteLine($"--> {item.Name}\n\t- ID: {item.Id}\n\t- Price: {item.Price}");
        }
        Console.WriteLine("------------------------------------------------");
    }
}
