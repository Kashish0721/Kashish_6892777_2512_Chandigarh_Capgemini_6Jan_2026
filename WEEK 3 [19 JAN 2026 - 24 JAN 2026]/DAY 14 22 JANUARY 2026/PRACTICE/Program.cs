using System;
using System.Text;

class Student
{
    public int Id = 1;
}

class Program
{
    static void Show(int x, int y = 5)
    {
        Console.WriteLine(x * y);
    }

    static void Info(string city, string name, int year)
    {
        Console.WriteLine($"{name} from {city} - {year}");
    }

    static void Main()
    {
        // 1. object Base Class
        object o = new Student();
        Console.WriteLine(o.GetType());
        Console.WriteLine(o.ToString());

        Console.WriteLine("---------------");

        // 2. Equals() vs ==
        string a = "Tech";
        string b = new string("Tech".ToCharArray());

        Console.WriteLine(a.Equals(b));
        Console.WriteLine(a == b);

        Console.WriteLine("---------------");

        // 3. String vs StringBuilder
        string s = "Hi";
        s.Replace("H", "B");
        Console.WriteLine(s);

        StringBuilder sb = new StringBuilder("Hi");
        sb.Replace("H", "B");
        Console.WriteLine(sb);

        Console.WriteLine("---------------");

        // 4. String Manipulation
        string data = "  CSharp Developer  ";
        string result = data.Trim().Substring(0, 6);
        Console.WriteLine(result);

        Console.WriteLine("---------------");

        // 5. String Class Methods
        string lang = "DotNet Programming";

        Console.WriteLine(lang.Contains("Net"));
        Console.WriteLine(lang.StartsWith("Dot"));
        Console.WriteLine(lang.IndexOf("Pro"));

        Console.WriteLine("---------------");

        // 6. Default Parameters
        Show(4);
        Show(4, 3);

        Console.WriteLine("---------------");

        // 7. Named Parameters
        Info(name: "Kashish", city: "Chandigarh", year: 2026);
    }
}
