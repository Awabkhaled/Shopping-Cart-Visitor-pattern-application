using System;
using System.Linq;

class OneLineFormatDescriptionVisitor : IProductVisitor
{
    public string Description { get; private set; }
    public void Visit(Clothing clothing)
    {
        string nullableDescription = "";
        if (clothing.Size.HasValue)
        {
            nullableDescription += clothing.Size.Value.ToString();
        }
        if (!string.IsNullOrEmpty(clothing.Color))
        {
            nullableDescription += " "+clothing.Color;
        }

        Description = $"{nullableDescription} {clothing.Name}, With Id {clothing.Id}.";
    }

    public void Visit(Toy toy)
    {
        string ageDescription = "";
        if (toy.LeastAge.HasValue)
        {
            ageDescription += ", Only allowed to +"+toy.LeastAge.ToString()+" years Old";
        }
        string materialDescription = "";
        if (!string.IsNullOrEmpty(toy.Material))
        {
            materialDescription += toy.Material;
        }

        Description = $"{materialDescription} {toy.Name}{ageDescription}, With Id {toy.Id}.";
    }

    public void Visit(Grocery grocery)
    {
        string weightDescription = "";
        if (grocery.Weight.HasValue)
        {
            weightDescription += grocery.Weight.ToString() + "kg of";
        }
        Description = $"{weightDescription} {grocery.Name}, With Id {grocery.Id}.";
    }
}
