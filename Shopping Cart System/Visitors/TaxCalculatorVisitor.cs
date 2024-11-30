using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TaxCalculatorVisitor: IProductVisitor
{
    public double TaxValue { get; private set; }

    public void Visit(Clothing clothing)
    {
        Database.Discounts.TryGetValue(clothing.Id, out double dicountPercentage);
        TaxValue = clothing.Price * dicountPercentage;
    }

    public void Visit(Toy toy)
    {
        Database.Discounts.TryGetValue(toy.Id, out double dicountPercentage);
        TaxValue = toy.Price * dicountPercentage;
    }

    public void Visit(Grocery grocery)
    {
        Database.Discounts.TryGetValue(grocery.Id, out double dicountPercentage);
        TaxValue = grocery.Price * dicountPercentage;
    }
}
