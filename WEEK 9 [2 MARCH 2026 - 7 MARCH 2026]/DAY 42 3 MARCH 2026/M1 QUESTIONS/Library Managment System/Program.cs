using System;
using System.Collections.Generic;
using System.Linq;

public interface IBook
{
    int Id { get; set; }
    string Title { get; set; }
    string Author { get; set; }
    string Category { get; set; }
    int Price { get; set; }
}

public interface ILibrarySystem
{
    void AddBook(IBook book, int quantity);
    void RemoveBook(IBook book, int quantity);
    int CalculateTotal();
    List<(string, int)> CategoryTotalPrice();
    List<(string, int, int)> BooksInfo();
    List<(string, string, int)> CategoryAndAuthorWithCount();
}

public class Book : IBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
}

public class LibrarySystem : ILibrarySystem
{
    private Dictionary<int, (IBook book, int quantity)> books =
        new Dictionary<int, (IBook, int)>();

    public void AddBook(IBook book, int quantity)
    {
        if (books.ContainsKey(book.Id))
            books[book.Id] = (book, books[book.Id].quantity + quantity);
        else
            books.Add(book.Id, (book, quantity));
    }

    public void RemoveBook(IBook book, int quantity)
    {
        if (!books.ContainsKey(book.Id))
            return;

        int newQty = books[book.Id].quantity - quantity;

        if (newQty <= 0)
            books.Remove(book.Id);
        else
            books[book.Id] = (book, newQty);
    }

    public int CalculateTotal()
    {
        return books.Values.Sum(x => x.book.Price * x.quantity);
    }

    public List<(string, int)> CategoryTotalPrice()
    {
        return books.Values
            .GroupBy(x => x.book.Category)
            .Select(g => (g.Key, g.Sum(x => x.book.Price * x.quantity)))
            .ToList();
    }

    public List<(string, int, int)> BooksInfo()
    {
        return books.Values
            .Select(x => (x.book.Title, x.quantity, x.book.Price))
            .ToList();
    }

    public List<(string, string, int)> CategoryAndAuthorWithCount()
    {
        return books.Values
            .GroupBy(x => new { x.book.Category, x.book.Author })
            .Select(g => (g.Key.Category, g.Key.Author, g.Sum(x => x.quantity)))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        ILibrarySystem library = new LibrarySystem();

        library.AddBook(new Book
        {
            Id = 1,
            Title = "PeterPan",
            Author = "JamesMatthewBarrie",
            Category = "KidsClassics",
            Price = 193
        }, 11);

        library.AddBook(new Book
        {
            Id = 2,
            Title = "TheWizardOfOz",
            Author = "FrankBaum",
            Category = "KidsClassics",
            Price = 394
        }, 3);

        Console.WriteLine("Book Info:");
        foreach (var b in library.BooksInfo())
            Console.WriteLine($"Book Name:{b.Item1}, Quantity:{b.Item2}, Price:{b.Item3}");

        Console.WriteLine("\nCategory Total Price:");
        foreach (var c in library.CategoryTotalPrice())
            Console.WriteLine($"Category:{c.Item1}, Total Price:{c.Item2}");

        Console.WriteLine("\nCategory And Author With Count:");
        foreach (var a in library.CategoryAndAuthorWithCount())
            Console.WriteLine($"Category:{a.Item1}, Author:{a.Item2}, Count:{a.Item3}");

        Console.WriteLine($"\nTotal Price: {library.CalculateTotal()}");

        Console.ReadLine();
    }
}