using System;


// Represents a Toy product
class Toy : ProductBase
{
    public override string Catagory { get; }
    private int? _leastAge;
    public int? LeastAge
    {
        get => _leastAge;
        set
        {
            if (!(value is null))
            {
                ValidationHelper.ValidateNumberInRange(value.GetValueOrDefault(), 0, 140, "Age");
            }
            _leastAge = value;
        }
    }
    public string? Material { get; set; }

    public Toy(string name, string description, double price, int leastAge, string material)
        : base(name, price, description)
    {
        LeastAge = leastAge;
        Material = material;
        Catagory = "Toys";
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "LeastAge", "Material" };
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n\t- Least Age: {LeastAge?.ToString() ?? "Not provided"}\n\t- Material: {Material}";
    }

    public override void Accept(IProductVisitor visitor)
    {
        visitor.Visit(this);
    }
}