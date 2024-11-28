using System;
using System.Collections.Generic;

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
