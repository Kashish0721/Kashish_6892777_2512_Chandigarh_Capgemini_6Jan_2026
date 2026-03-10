using System;
using System.Collections.Generic;
using System.Linq;

struct Product
{
    public string product_id;
    public int sales;
}

class Program
{
    static void Main()
    {
        Dictionary<string, int> map = new Dictionary<string, int>();

        Console.WriteLine("Enter product records (type END to stop):");

        while (true)
        {
            string input = Console.ReadLine();

            if (input.ToUpper() == "END")
                break;

            string[] parts = input.Split('-');

            string id = parts[0];
            int sales = int.Parse(parts[1]);

            if (!map.ContainsKey(id) || map[id] < sales)
                map[id] = sales;
        }

        var sorted = map.OrderByDescending(x => x.Value);

        foreach (var item in sorted)
        {
            Console.WriteLine($"{item.Key}-{item.Value}");
        }
    }
}
