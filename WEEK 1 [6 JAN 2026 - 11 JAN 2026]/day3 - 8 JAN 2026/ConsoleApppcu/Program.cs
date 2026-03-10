namespace ConsoleApppcu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int x, y;
        Console.Write("enter the x value");
        x = int.Parse(Console.ReadLine());
        Console.Write("enter the y value");
        y = int.Parse(Console.ReadLine());
        if (x > y)
        {
            Console.WriteLine("x is greater than y");
        }
        else if (x < y)
        {
            Console.WriteLine("x is less than y");
        }
        else
        {
            Console.WriteLine("x is equal to y");
        }
        Console.ReadLine();
    }
}