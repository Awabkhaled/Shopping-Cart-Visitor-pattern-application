/*
 * A file that contains all classes related to the products and their validation
 * Product: Abstract base class for all product types with common properties and validation
 * Clothing: A class representing clothing products with additional properties like size and color
 * Toy: A class representing toy products with properties like least age and material
 * Grocery: A class representing grocery products with properties like weight
 * ValidationHelper: A helper class for validating values (integer ranges, double ranges, and enum values)
 */
using System;
using System.Collections.Generic;
using System.Drawing;
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
    private static int IdCounter = 1;
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

    public Product(string name, double price,string description)
    {
        Name = name;
        Description = description;
        Price = price;
        Id = IdCounter++;
    }

    public override string ToString()
    {
        return $"Id: {Id},\nName: {Name},\nDescription: {Description},\nPrice: {Price}";
    }

    // specific formatting for listing
    public string ToListString()
    {
        return $"{{\nId: {Id},\nName: {Name},\nPrice: {Price}\n}}";
    }
}


// Represents a clothing product
class Clothing : Product
{
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
        : base(name, price, description)
    {
        Color = color;
        Size = size;
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "Size", "Color" };
    }

    public override string ToString()
    {
        string size = Size.HasValue ? Size.ToString() : "Not provided";
        return $"{{\n{base.ToString()},\nSize: {size},\nColor: {Color}\n}}";
    }
}


// Represents a Toy product
class Toy : Product
{
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
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "LeastAge", "Material" };
    }

    public override string ToString()
    {
        return $"{{\n{base.ToString()},\nLeast Age: {LeastAge?.ToString() ?? "Not provided"},\nMaterial: {Material}\n}}";
    }
}


// Represents a Grocery product
class Grocery : Product
{
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
    }

    // names of Fields that I will ask the user to enter in a specific order (order of the constructor)
    public static string[] FieldsToAskUserFor()
    {
        return new string[] { "Name", "Description", "Price", "Weight" };
    }

    public override string ToString()
    {
        return $"{{\n{base.ToString()},\nWeight: {Weight?.ToString() ?? "Not provided"} kg\n}}";
    }
}
