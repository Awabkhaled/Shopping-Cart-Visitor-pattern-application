using System;
using static Clothing;

// Represents a clothing product
class Clothing : ProductBase
{
    public override string Catagory { get; }
    public enum Sizes
    {
        Small = 1,
        Medium = 2,
        Large = 3,
        Xlarge = 4,
        XXlarge = 5
    }
    public string? Color { get; set; }
    private Sizes? _size;
    public Sizes? Size
    {
        get => _size;
        set
        {
            if (value.HasValue)
            {
                ValidationHelper.ValidateValueInEnum(value.GetValueOrDefault());
            }
            _size = value;
        }
    }
    public Clothing(string name, string description, double price, Sizes size, string color)
        : base(name, price, description)
    {
        Color = color;
        Size = size;
        Catagory = "Clothing";
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "Size", "Color" };
    }

    public override string ToString()
    {
        var size = Size.HasValue ? Size.ToString() : "Not provided";
        return $"{base.ToString()}\n\t- Size: {size}\n\t- Color: {Color}";
    }

    public override void Accept(IProductVisitor visitor)
    {
        visitor.Visit(this);
    }
}
