using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string str = "ABCDEF";

        Dictionary<int, string> inserts = new Dictionary<int, string>()
        {
            {0,"Hello"},
            {5,"World"},
            {str.Length,"!"}
        };

        foreach (var item in inserts.OrderByDescending(x => x.Key))
        {
            str = str.Insert(item.Key, item.Value);
        }

        Console.WriteLine(str);
    }
}
