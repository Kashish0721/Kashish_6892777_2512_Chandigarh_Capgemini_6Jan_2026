using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

abstract class GroceryReceiptBase2
{
    public Dictionary<string, decimal> Prices { get; set; }
    public Dictionary<string, int> Discounts { get; set; }

    public GroceryReceiptBase2(Dictionary<string, decimal> prices,
                               Dictionary<string, int> discounts)
    {
        Prices = prices;
        Discounts = discounts;
    }

    public abstract IEnumerable<(string fruit, decimal price, decimal total)>
        Calculate(List<Tuple<string, int>> shoppingList);
}

class GroceryReceipt2 : GroceryReceiptBase2
{
    public GroceryReceipt2(Dictionary<string, decimal> prices,
                           Dictionary<string, int> discounts)
        : base(prices, discounts)
    {
    }

    public override IEnumerable<(string fruit, decimal price, decimal total)>
        Calculate(List<Tuple<string, int>> shoppingList)
    {
        foreach (var item in shoppingList)
        {
            string fruit = item.Item1;
            int quantity = item.Item2;

            decimal price = Prices[fruit];
            decimal total = price * quantity;

            // Apply discount if exists
            if (Discounts.ContainsKey(fruit))
            {
                int discount = Discounts[fruit];
                total = total - (total * discount / 100);
            }

            yield return (fruit, price, total);
        }
    }
}

class Program
{
    static void Main()
    {
        var prices = new Dictionary<string, decimal>()
        {
            {"Banana", 10},
            {"Apple", 20},
            {"Orange", 8}
        };

        var discounts = new Dictionary<string, int>()
        {
            {"Orange", 10}
        };

        var shoppingList = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("Banana", 2),
            new Tuple<string, int>("Apple", 3),
            new Tuple<string, int>("Orange", 5)
        };

        var receipt = new GroceryReceipt2(prices, discounts);

        foreach (var item in receipt.Calculate(shoppingList))
        {
            Console.WriteLine($"{item.fruit} {item.price} {item.total}");
        }
    }
}