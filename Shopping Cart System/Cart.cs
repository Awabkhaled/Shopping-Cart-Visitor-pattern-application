/*
 * A file that contains all classes related to the Cart
 * CartItem: A class for each item in the Shopping Cart
 * ShoppingCart: A class the whole shopping cart
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CartItem
{
    public Product TheProduct { get; set; }
    private int _quantity;
    public int Quantity
    {
        get => _quantity; set => _quantity = value;
    }
    public double TotalPrice
    {
        get
        {
            return _quantity * TheProduct.Price;
        }
    }
    public void AddAmount(int amount=1)
    {
        _quantity += amount;
    }

    public CartItem(Product product, int quantity=1) 
    {
        TheProduct = product ?? throw new ArgumentNullException(nameof(product));
        AddAmount(quantity);
    }

    public override string ToString()
    {
        return $"{{\nProduct name: {TheProduct.Name},\nId: {TheProduct.Id},\nQuantity: {Quantity},\nTotal Price: {TotalPrice:C}\n}}";
    }
}



class ShoppingCart
{
    private readonly List<CartItem> CartItems;

    public ShoppingCart()
    {
        CartItems = new List<CartItem>();
    }

    // Ading a product to the shopping cart
    public void AddItem(Product product, int quantity) 
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        if (quantity < 1)
            throw new ArgumentException("Quantity must be greater than zero.");
        var existingitem = CartItems.FirstOrDefault(item => item.TheProduct.Id == product.Id);
        if(existingitem != null)
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
    }

    // Calculate the whole Total Price
    public double CalculateTotalPrice()
    {
        return CartItems.Sum(item => item.TotalPrice); 
    }

    // Clearing Cart
    public void ClearCart()
    {
        CartItems.Clear();
    }

    // List all the Item in the cart
    public List<string> ListAllItems()
    {
        return CartItems.Select(item => item.ToString()).ToList();
    }
}