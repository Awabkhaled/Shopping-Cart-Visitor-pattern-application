/*
 * A file that contains The abstract class for all classes that is related to products 
 */
using System;

// Abstract base class for all product types
abstract class ProductBase
{
    private static int IdCounter = 1;
    public readonly int Id;
    private double _discountPercentage;

    public double DiscountPercentage 
    {
        get
        {
            Database.Discounts.TryGetValue(Id, out double discountPercentage);
            return discountPercentage;
        }
    }
    private string _name;
    public string Name {
        get => _name;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Name cannot exceed 100 characters.");
            }
            _name = value;
        }
    }
    public string? Description { get; set; }
    private double _price;
    public double Price
    {
        get => _price;
        set
        {
             ValidationHelper.ValidateNumberInRange(value, 0, 1000000, "Price");
            _price = value;
        }
    }
    public abstract string Catagory { get; }
    public ProductBase(string name, double price,string description)
    {
        Name = name;
        Description = description;
        Price = price;
        Id = IdCounter++;
    }

    private string ToBaseString()
    {
        double discountPercentage = DiscountPercentage;
        string discountString = "";
        if (discountPercentage != 0)
        {
            discountString = $"\n\t- Discount Precent: {discountPercentage}";
        }
        string finalString = $"--> {Name}\n\t- ID: {Id}\n\t- Price: {Price}\n\t- Catagory: {Catagory}{discountString}";
        return finalString;
    }
    public override string ToString()
    {
        return ToBaseString();
    }

    // specific formatting for listing
    public string ToListString()
    {
        return ToBaseString();
    }

    public abstract void Accept(IProductVisitor visitor);
}
