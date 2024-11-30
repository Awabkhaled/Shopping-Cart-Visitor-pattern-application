using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        Database.InitializeDatabase();
        ShoppingCart shoppingCart = new ShoppingCart();
        shoppingCart.AddItem(1, 2);
        shoppingCart.AddItem(2, 1);
        shoppingCart.AddItem(3, 3);
        shoppingCart.AddItem(4, 4);
        shoppingCart.AddItem(6, 1);
        shoppingCart.ListCartItems();
        Console.WriteLine("\n\n\n"); 
        shoppingCart.ApplyDiscounts();
        shoppingCart.ListCartItems();
        Console.WriteLine("\n\n\n");
        shoppingCart.ApplyTax();
        shoppingCart.ListCartItems();
    }
}
