using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Represents a Grocery product
class Grocery : ProductBase
{
    public override string Catagory { get; }
    private double? _weight;
    public double? Weight
    {
        get => _weight;
        set
        {
            if (!(value is null))
            {
                ValidationHelper.ValidateNumberInRange(value.GetValueOrDefault(), 0, 1000000, "Weight");
            }
            _weight = value;
        }
    }
    public Grocery(string name, string description, double price, double weight)
        : base(name, price, description)
    {
        Weight = weight;
        Catagory = nameof(Grocery);
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "Weight" };
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n\t- Weight: {Weight?.ToString() ?? "Not provided"} kg";
    }

    public override void Accept(IProductVisitor visitor)
    {
        visitor.Visit(this);
    }
}
