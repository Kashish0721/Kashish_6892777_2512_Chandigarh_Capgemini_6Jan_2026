using System;

class Program
{
    static void Main()
    {
        Electronics e = new Electronics();
        e.Name = "Laptop";
        e.Price = 60000;
        e.Show();

        Clothing c = new Clothing();
        c.Name = "Jacket";
        c.Price = 2500;
        c.Show();

        Books b = new Books();
        b.Name = "C# Guide";
        b.Price = 499;
        b.Show();
    }
}
