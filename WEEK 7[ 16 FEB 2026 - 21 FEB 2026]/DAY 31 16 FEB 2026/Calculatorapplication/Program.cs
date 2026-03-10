namespace Calculatorapplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();
            Console.WriteLine($"Add: {calc.Add(5, 2)}");
            Console.WriteLine($"Subtract: {calc.Subtract(10,2)}");
            Console.WriteLine($"Multiply: {calc.Multiply(5, 2)}");
            Console.WriteLine($"Divide: { calc.Divide(10,2)}");
        }
    }
}
