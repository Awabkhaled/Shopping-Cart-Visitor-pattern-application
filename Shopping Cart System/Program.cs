using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.FileIO;

class Program
{
    private const int NO_ID_CONSTANT = -1;
    static void ShowALlProducts(string? productType)
    {
        List<ProductBase> products;
        string message="";
        if (string.IsNullOrEmpty(productType)) 
        {
            products = Database.Products;
            message = $"\t\t\t************ Listing ALL PRODUCTS ******************";
        }
        else
        {
            products = Database.Products.FindAll(item=>item.Catagory==productType);
            message = $"*** Listing {productType.ToUpper()} PRODUCTS ***";
        }
        Console.WriteLine(message);

        foreach (ProductBase product in products)
        {
            Console.WriteLine(product.ToListString() + "\n");
        }
    }
    static void FilterProductsByType()
    {
        Console.WriteLine($"\t\t\t************ FILTERING PRODUCTS BY CATAGORY ******************");
        int option = 0;
        Console.WriteLine("Choose a Catagory: ");
        Console.WriteLine("1. Clothing");
        Console.WriteLine("2. Toys");
        Console.WriteLine("3. Grocery");
        Console.WriteLine("4. Return To Main Minu");
        Console.Write("Enter Option Number: ");
        option = ValidationHelper.ValidateOption(Console.ReadLine(), 4, 1);
        while (option == 0)
        {
            Console.Write("(ERROR) Please Enter A Valid Option: ");
            option = ValidationHelper.ValidateOption(Console.ReadLine(), 4, 1);
        }
        Console.WriteLine();
        switch (option)
        {
            case 1:
                ShowALlProducts(nameof(Clothing));
                break;
            case 2:
                ShowALlProducts(nameof(Toy));
                break;
            case 3:
                ShowALlProducts(nameof(Grocery));
                break;
            case 4:
                break;
            default:
                Console.WriteLine("Something went wrong with the system");
                break;
        }
    }
    static void ProductSpecifOperation()
    {
        Console.WriteLine("\t\t\t************ A SPECIFIC PRODUCT Details ******************");
        int id = RecieveIdForProduct(Database.Products);
        if (id != NO_ID_CONSTANT)
        {
            Console.WriteLine();
            Console.WriteLine($"*** Product With ID {id} details ***");
            Console.WriteLine(Database.Products.Find(item=>item.Id == id).ToString());
        }
    }
    static void ShoppingCartOperations(ShoppingCart shoppingCart)
    {
        Console.WriteLine($"\t\t\t************ Shopping Cart ******************");
        int option = 0;
        while (option != 8)
        {
            Console.WriteLine("SHOPPING CART OPTIONS: ");
            Console.WriteLine("1. List All Products In The Cart");
            Console.WriteLine("2. Add Item To The Cart");
            Console.WriteLine("3. Remove Item From The Cart");
            Console.WriteLine("4. Update The Quantity Of An Item");
            Console.WriteLine("5. Apply Discounts For All The Items");
            Console.WriteLine("6. Finish Order");
            Console.WriteLine("7. Clear The Cart");
            Console.WriteLine("8. Return To MAIN MENU");
            Console.Write("Enter Option Number: ");
            option = ValidationHelper.ValidateOption(Console.ReadLine(), 8, 1);
            while (option == 0)
            {
                Console.Write("(ERROR) Please Enter A Valid Option: ");
                option = ValidationHelper.ValidateOption(Console.ReadLine(), 8, 1);
            }
            Console.WriteLine();
            switch (option)
            {
                case 1:
                    shoppingCart.ListCartItems();
                    break;
                case 2:
                    Console.WriteLine("**Adding Item**");
                    int id = RecieveIdForProduct(Database.Products);
                    int quantity = NO_ID_CONSTANT;
                    if (id != NO_ID_CONSTANT)
                    {
                        quantity = ReciveQuantityForProduct();
                    }
                    if(id == NO_ID_CONSTANT || quantity == NO_ID_CONSTANT)
                    {
                        Console.WriteLine();
                        continue;
                    }
                    shoppingCart.AddItem(id, quantity);
                    break;
                case 3:
                    Console.WriteLine("**Removing Item**");
                    id = RecieveIdForProduct(shoppingCart.returnProducts());
                    if(id == NO_ID_CONSTANT)
                    {
                        Console.WriteLine();
                        continue;
                    }
                    shoppingCart.RemoveItem(id);
                    break;
                case 4:
                    Console.WriteLine("**Updating Quantity**");
                    id = RecieveIdForProduct(shoppingCart.returnProducts());
                    quantity = NO_ID_CONSTANT;
                    if(id != NO_ID_CONSTANT)
                    {
                        quantity = ReciveQuantityForProduct();
                    }
                    if (id == NO_ID_CONSTANT || quantity == NO_ID_CONSTANT)
                    {
                        Console.WriteLine();
                        continue;
                    }
                    shoppingCart.UpdateQuantity(id, quantity);
                    break;
                case 5:
                    Console.WriteLine("**Applyinh Discounts**");
                    string message = "";
                    message =  shoppingCart.ApplyDiscounts()? "Discounts Applied Succefully!!!": "Discounts Already Applied!!!";
                    Console.WriteLine(message);
                    break;
                case 6:
                    Console.WriteLine("\n\n**************** THE RECEIPT ****************");
                    foreach(var item in shoppingCart.CartItems)
                    {
                        Console.WriteLine("* ->"+item.OneLineDescription());
                    }
                    Console.WriteLine("* Total Price: " + $"{shoppingCart.CalculateTotalPrice():C}");
                    shoppingCart.ApplyTax();
                    Console.WriteLine("* Total Price After Taxs: " + $"{shoppingCart.CalculateTotalPrice():C}");
                    Console.WriteLine("*********************************************");
                    Environment.Exit(0);
                    break;
                case 7:
                    Console.WriteLine("**Clearing Cart**");
                    shoppingCart.ClearCart();
                    Console.WriteLine("Cart Cleared Succefully");
                    break;
                case 8:
                    continue;
                default:
                    Console.WriteLine("Something went wrong with the system");
                    break;
            }
            Console.WriteLine();

        }
    }
    // A helper function That take a string for id and see if it is in the
    // database and ask the user tell he enter a valid id
    static private int RecieveIdForProduct(List<ProductBase> products)
    {
        int[] ids = products.Select(item => item.Id).ToArray();
        string idsString = $"({string.Join(", ", ids)})";
        Console.WriteLine($"Available ID's: {idsString}");
        Console.Write("Enter ID (enter -1 to return): ");
        string idString = "";
        idString = Console.ReadLine();
        bool exist = ValidationHelper.ValidateNumberInArray(ids, idString);
        while (exist == false)
        {
            Console.Write("(ERROR) Please Enter A Valid Value: ");
            idString = Console.ReadLine();
            exist = ValidationHelper.ValidateNumberInArray(ids, idString);
        }
        return int.Parse(idString);
    }
    
    static private int ReciveQuantityForProduct()
    {
        Console.Write("Enter The new Quantity (enter -1 to return): ");
        string quantityString = "";
        quantityString = Console.ReadLine();
        while (true)
        {
            if(int.TryParse(quantityString, out int result))
            {
                if(result > 0 || result == -1)
                {
                    return result;
                }
            }
            Console.Write("(ERROR) Please Enter A Valid Value: ");
            quantityString = Console.ReadLine();
        }
    }
    
    static void Main(string[] args)
    {
        Database.InitializeDatabase();
        ShoppingCart shoppingCart = new ShoppingCart();
        Dictionary<int, string> map = new Dictionary<int, string>();
        int option=0;
        Console.WriteLine("\t\t\t*************** Hello To My Shopping Cart System ***************\n");
        while (option != 5)
        {
            Console.WriteLine("-----> MAIN MENU Options <-----");
            Console.WriteLine("1. List All Products");
            Console.WriteLine("2. Filter Products by Type");
            Console.WriteLine("3. List a Product Details");
            Console.WriteLine("4. Shopping Cart Operations");
            Console.WriteLine("5. EXIT");
            Console.Write("Enter Option Number: ");
            option = ValidationHelper.ValidateOption(Console.ReadLine(), 5, 1);
            while (option==0)
            {
                Console.Write("(ERROR) Please Enter A Valid Option: ");
                option = ValidationHelper.ValidateOption(Console.ReadLine(), 5, 1);
            }
            Console.WriteLine();
            switch(option)
            {
                case 1: 
                    ShowALlProducts(null);
                    break;
                case 2:
                    FilterProductsByType();
                    break;
                case 3:
                    ProductSpecifOperation();
                    break;
                case 4:
                    ShoppingCartOperations(shoppingCart);
                    break;
                case 5:
                    continue;
                default: 
                    Console.WriteLine("Something went wrong with the system");
                    break;
            }
            Console.WriteLine("\t\t\t*****************************************************\n\n");
        }

    }
}
