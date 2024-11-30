using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Database
{
    public static List<ProductBase> Products = new List<ProductBase>();

    // dictionary for items with their discount
    public static Dictionary<int, double> Discounts = new Dictionary<int, double>();

    // dictionary for Product Types with their Tax
    public static Dictionary<string, double> Taxs = new Dictionary<string, double>();



    public static void InitializeDatabase()
    {
        Products.Add(new Clothing("T-Shirt", "Cotton T-Shirt", 100.0, Clothing.Sizes.Medium, "Blue"));
        Products.Add(new Clothing("Pants", "Cotton Pants", 152.0, Clothing.Sizes.XXlarge, "Black"));
        Products.Add(new Toy("Lego Set", "Lego Building Blocks", 100.0, 6, "Plastic"));
        Products.Add(new Toy("Luffy Action Figure", "Figure for Monkey D Luffy From One Piece", 2050.0, 14, "Plastic, Fibers"));
        Products.Add(new Grocery("Apple", "Fresh Green Apples", 45.0, 4.0));
        Products.Add(new Grocery("Tomato", "Fresh Red Tomato", 10.25, 1.5));

        // set discounts for some items
        SetDiscount("T-Shirt", 0.20);
        SetDiscount("Apple", 0.35);
        SetDiscount("Luffy Action Figure", 0.10);

        // set Taxes for each product type
        Taxs["Clothing"] = 0.2;
        Taxs["Toy"] = 0.3;
        Taxs["Grocery"] = 0.12;
    }

    private static void SetDiscount(string productName, double discountPercintage)
    {
        ProductBase? productWithDiscount = Products.FirstOrDefault(item => item.Name == productName);
        if ( productWithDiscount != null)
        {
            Discounts[productWithDiscount.Id] = discountPercintage;
        }
    }
}

