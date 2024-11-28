using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DiscountCalculatorVisitor: IProductVisitor
{
    public double Discount { get; private set; }

    public void Visit(Clothing clothing)
    {
        Database.Discounts.TryGetValue(clothing.Id, out double dicountPercentage);
        Discount = clothing.Price * dicountPercentage;
    }

    public void Visit(Toy toy)
    {
        Database.Discounts.TryGetValue(toy.Id, out double dicountPercentage);
        Discount = toy.Price * dicountPercentage;
    }

    public void Visit(Grocery grocery)
    {
        Database.Discounts.TryGetValue(grocery.Id, out double dicountPercentage);
        Discount = grocery.Price * dicountPercentage;
    }
}
