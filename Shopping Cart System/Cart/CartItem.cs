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
    public ProductBase TheProduct { get; set; }
    public double TotalPriceAfterDiscount { get; private set; }
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

    public CartItem(ProductBase product, int quantity=1) 
    {
        TheProduct = product ?? throw new ArgumentNullException(nameof(product));
        AddAmount(quantity);
        TotalPriceAfterDiscount = TotalPrice;
    }

    public void ApplyDiscount()
    {
        Database.Discounts.TryGetValue(TheProduct.Id, out double dicountPercentage);
        double discount = TheProduct.Price * dicountPercentage;
        TotalPriceAfterDiscount = TotalPrice - (discount*Quantity);
    }

    public void ApplyTax()
    {
        TaxCalculatorVisitor visitor = new TaxCalculatorVisitor();
        TheProduct.Accept(visitor);
        TotalPriceAfterDiscount += visitor.TaxValue;
    }

    public override string ToString()
    {
        string discountString = $"\n\t- Total Price: {TotalPrice:C}";
        if (TotalPriceAfterDiscount != TotalPrice)
        {
            discountString = $"\n\t- Total Price Before Discount: {TotalPrice:C}" +
                $"\n\t- Total Price After Discount: {TotalPriceAfterDiscount:C}";
        }
        string finalString = $"{TheProduct.ToString()}\n\t- Quantity: {Quantity}{discountString}";
        return finalString;
    }
}
