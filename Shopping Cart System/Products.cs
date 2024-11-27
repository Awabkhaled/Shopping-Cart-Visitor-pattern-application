using System;
using System.Collections.Generic;
using static Clothing;

// Contains Helper methods for validating values
public static class ValidationHelper
{
    // Validate if an integer is within a specified range
    public static void ValidateNumberInRange(int value, int minValue, int maxValue, string parameterName)
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentException($"{parameterName} must be between {minValue} and {maxValue}.");
        }
    }

    // Validate if a double is within a specified range
    public static void ValidateNumberInRange(double value, double minValue, double maxValue, string parameterName)
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentException($"{parameterName} must be between {minValue} and {maxValue}.");
        }
    }

    // Validate if a value exists in an enumerator
    public static void ValidateValueInEnum<TEnum>(TEnum value) where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            string enumPossibleValues = string.Join(", ", Enum.GetNames(typeof(TEnum)));
            string message = $"Invalid value. The value must be one of: ({enumPossibleValues}).";
            throw new ArgumentException(message);
        }
    }
}


// Abstract base class for all product types
abstract class Product
{
    
    public readonly int Id;
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
    private double? _price;
    public double? Price
    {
        get => _price;
        set
        {
            if (!(value is null))
            {
                ValidationHelper.ValidateNumberInRange(value.GetValueOrDefault(), 0, 1000000, "Price");
            }
            _price = value;
        }
    }

    public Product(string name, double price,string description, int id)
    {
        Name = name;
        Description = description;
        Price = price;
        Id = id;
    }
}


// Represents a clothing product
class Clothing : Product
{
    private static int IdCounter = 1;
    public enum Sizes
    {
        Small = 1,
        Medium = 2,
        Large = 3,
        Xlarge = 4,
        XXlarge = 5
    }
    public string? Color {  get; set; }
    private Sizes? _size;
    public Sizes? Size {
        get => _size;
        set 
        {
            if (value.HasValue) {
                ValidationHelper.ValidateValueInEnum(value.GetValueOrDefault());
            }
            _size = value;
        }
    }
    public Clothing(string name, string description, double price, Sizes size, string color)
        : base(name, price, description, IdCounter)
    {
        Color = color;
        Size = size;
        IdCounter++;
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "Size", "Color" };
    }
}


// Represents a Toy product
class Toy : Product
{
    private static int IdCounter = 1;
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
        : base(name, price, description, IdCounter)
    {
        LeastAge = leastAge;
        Material = material;
        IdCounter++;
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "LeastAge", "Material" };
    }
}


// Represents a Grocery product
class Grocery : Product
{
    private static int IdCounter = 1;
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
        : base(name, price, description, IdCounter)
    {
        Weight = weight;
        IdCounter++;
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "Weight" };
    }
}
