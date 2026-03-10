using NUnit.Framework;
using LibraryApp;
using System;

[TestFixture]
public class LibraryTests
{
    private Library library;

    [SetUp]
    public void Setup()
    {
        library = new Library();
        library.AddBook("C# Basics", 2);
    }

    [Test]
    public void AddBook_NewBook_IncreasesCount()
    {
        // Arrange
        string title = "ASP.NET Core";
        int quantity = 3;

        // Act
        library.AddBook(title, quantity);

        // Assert
        Assert.That(library.GetBookCount(title), Is.EqualTo(3));
    }

    [Test]
    public void BorrowBook_ExistingBook_DecreasesCount()
    {
        // Act
        library.BorrowBook("C# Basics");

        // Assert
        Assert.That(library.GetBookCount("C# Basics"), Is.EqualTo(1));
    }

    [Test]
    public void BorrowBook_NotAvailable_ThrowsException()
    {
        // Act & Assert
        Assert.That(() => library.BorrowBook("Java"),
                    Throws.TypeOf<InvalidOperationException>());
    }
}
