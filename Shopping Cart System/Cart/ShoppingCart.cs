using System;
using System.Collections.Generic;
using System.Linq;

class ShoppingCart
{
    public readonly List<CartItem> CartItems;
    private bool DiscountApplied = false;

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
            existingItem.Quantity = quantity;
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
        return CartItems.Sum(item => item.DiscountApplied? item.TotalPriceAfterDiscount: item.TotalPrice);
    }

    // Clearing Cart
    public void ClearCart()
    {
        CartItems.Clear();
        DiscountApplied = false;
    }

    // List all the Item in the cart
    public void ListCartItems()
    {
        Console.WriteLine("***************The Shopping Cart***************");
        foreach (var item in CartItems)
        {
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine($"----> The Whole Cart Price: {CalculateTotalPrice():C}");
        Console.WriteLine("**********************************************");
    }

    public bool ApplyDiscounts()
    {
        if (!DiscountApplied)
        {
            foreach (var item in CartItems)
            {
                item.ApplyDiscount();
            }
            DiscountApplied = true;
            return true;
        }
        return false;
    }

    public void ApplyTax()
    {
        foreach (var item in CartItems)
        {
            item.ApplyTax();
        }
    }

    public List<ProductBase> returnProducts()
    {
        List<ProductBase> products = new List<ProductBase>();
        foreach (var item in CartItems)
        {
            products.Add(item.TheProduct);
        }
        return products;
    }
}
