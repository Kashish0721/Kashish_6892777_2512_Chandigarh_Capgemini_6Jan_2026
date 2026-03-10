using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Book> inventory = new List<Book>
        {
            new Book("C# Basics", 299, 5),
            new Book("LINQ Guide", 499, 0),
            new Book("DSA", 399, 3)
        };

        // Add new book
        inventory.Add(new Book("OOP Concepts", 250, 4));

        // Find books cheaper than target price
        int target = 350;
        var cheapBooks = inventory.Where(b => b.Price < target);
        Console.WriteLine("Books cheaper than " + target);
        foreach (var b in cheapBooks)
            Console.WriteLine(b.Name);

        // Increase price by 10%
        inventory = inventory.Select(b =>
        {
            b.Price += (int)(b.Price * 0.10);
            return b;
        }).ToList();

        // Remove out-of-stock
        inventory.RemoveAll(b => b.Stock == 0);

        Console.WriteLine("\nFinal Inventory:");
        foreach (var b in inventory)
            Console.WriteLine($"{b.Name} - {b.Price} - Stock:{b.Stock}");
    }
}

class Book
{
    public string Name;
    public int Price;
    public int Stock;

    public Book(string n, int p, int s)
    {
        Name = n; Price = p; Stock = s;
    }
}
