using System;

interface IProductVisitor
{
    void Visit(Clothing clothing);
    void Visit(Toy toy);
    void Visit(Grocery grocery);
}
