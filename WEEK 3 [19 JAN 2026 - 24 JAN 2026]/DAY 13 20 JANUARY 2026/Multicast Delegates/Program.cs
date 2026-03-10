using System;

public delegate void Math(int x, int y);

class MultiClass
{
    public void Add(int x, int y)
    {
        Console.WriteLine("Add: " + (x + y));
    }

    public void Subtract(int x, int y)
    {
        Console.WriteLine("Subtract: " + (x - y));
    }

    public void Multiply(int x, int y)
    {
        Console.WriteLine("Multiply: " + (x * y));
    }

    public void Divide(int x, int y)
    {
        Console.WriteLine("Divide: " + (x / y));
    }
}

class Program
{
    static void Main()
    {
        MultiClass obj = new MultiClass();

        Math m = new Math(obj.Add);
        m += obj.Subtract;
        m += obj.Multiply;
        m += obj.Divide;

        m(100, 50);
        Console.WriteLine();

        m(450, 70);
        Console.WriteLine();

        m -= obj.Divide;                                                                                            bbb     
        m(625, 25);

        Console.ReadLine();
    }
}
