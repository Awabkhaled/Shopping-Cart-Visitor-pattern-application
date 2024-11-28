using System;
using System.Collections.Generic;
using System.Linq;

class ShoppingCart
{
    private readonly List<CartItem> CartItems;

    public ShoppingCart()
    {
        CartItems = new List<CartItem>();
    }

    // Ading a product to the shopping cart
    public void AddItem(int productId, int quantity)
    {
        var product = Database.Products.Find(item => item.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        if (quantity < 1)
            throw new ArgumentException("Quantity must be greater than zero.");
        var existingitem = CartItems.FirstOrDefault(item => item.TheProduct.Id == productId);
        if (existingitem != null)
        {
            existingitem.AddAmount(quantity);
        }
        else
        {
            CartItems.Add(new CartItem(product, quantity));
        }
    }

    // Removing an item of the cart with the id
    public void RemoveItem(int productId)
    {
        var existingItem = CartItems.FirstOrDefault(item => item.TheProduct.Id == productId);
        if (existingItem == null)
        {
            Console.WriteLine("Item not found in the cart.");
            return;
        }
        CartItems.Remove(existingItem);
    }

    // Apdate the quantity of an item using ID
    public void UpdateQuantity(int productId, int quantity)
    {
        var existingItem = CartItems.FirstOrDefault(item => item.TheProduct.Id == productId);
        if (existingItem != null)
        {
            existingItem.AddAmount(quantity);
        }
        else
        {
            Console.WriteLine("Item not found in the cart.");
            return;
        }
    }

    // Calculate the whole Total Price
    public double CalculateTotalPrice()
    {
        return CartItems.Sum(item => item.TotalPriceAfterDiscount);
    }

    // Clearing Cart
    public void ClearCart()
    {
        CartItems.Clear();
    }

    // List all the Item in the cart
    public void ListCartItems()
    {
        foreach (var item in CartItems)
        {
            Console.WriteLine(item.ToString());
        }
    }

    public void ApplyDiscounts()
    {
        foreach (var item in CartItems)
        {
            item.ApplyDiscount();
        }
    }
}
