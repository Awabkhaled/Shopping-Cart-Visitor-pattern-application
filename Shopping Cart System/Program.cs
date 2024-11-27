using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create some products
        var shirt = new Clothing("T-Shirt", "A comfy cotton shirt", 19.99, Clothing.Sizes.Medium, "Red");
        var toy = new Toy("Action Figure", "A cool superhero action figure", 9.99, 5, "Plastic");
        var apple = new Grocery("Apple", "Fresh red apple", 0.99, 0.2);

        // Create a shopping cart
        var cart = new ShoppingCart();

        // Add products to the shopping cart
        Console.WriteLine("Adding items to cart...");
        cart.AddItem(shirt, 2);   // Add 2 T-Shirts
        cart.AddItem(toy, 1);     // Add 1 Toy
        cart.AddItem(apple, 5);   // Add 5 Apples

        // List all items in the cart
        Console.WriteLine("\nItems in the cart:");
        var items = cart.ListAllItems();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }

        // Calculate the total price of the cart
        Console.WriteLine($"\nTotal Price: {cart.CalculateTotalPrice():C}");

        // Update the quantity of a product
        Console.WriteLine("\nUpdating the quantity of the T-Shirt...");
        cart.UpdateQuantity(shirt.Id, 3); // Change the quantity of T-Shirt to 3

        // List all items again to see the update
        Console.WriteLine("\nItems in the cart after update:");
        items = cart.ListAllItems();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }

        // Remove an item from the cart
        Console.WriteLine("\nRemoving the Toy from the cart...");
        cart.RemoveItem(toy.Id);

        // List all items again to see the removal
        Console.WriteLine("\nItems in the cart after removal:");
        items = cart.ListAllItems();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }

        // Calculate the total price again after removal
        Console.WriteLine($"\nTotal Price after removal: {cart.CalculateTotalPrice():C}");

        // Clear the cart
        Console.WriteLine("\nClearing the cart...");
        cart.ClearCart();

        // List all items to confirm cart is cleared
        Console.WriteLine("\nItems in the cart after clearing:");
        items = cart.ListAllItems();
        if (items.Count == 0)
        {
            Console.WriteLine("The cart is empty.");
        }
    }
}
