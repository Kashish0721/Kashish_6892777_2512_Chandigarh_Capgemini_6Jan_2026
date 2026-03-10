using System;

class Program
{
    static void Main(string[] args)
    {
        DiscountService service = new DiscountService();

        Console.WriteLine("Enter cart total:");
        decimal total = Convert.ToDecimal(Console.ReadLine());

        decimal finalAmount = service.ApplyDiscount(total);

        Console.WriteLine($"Final Amount after discount: {finalAmount}");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
