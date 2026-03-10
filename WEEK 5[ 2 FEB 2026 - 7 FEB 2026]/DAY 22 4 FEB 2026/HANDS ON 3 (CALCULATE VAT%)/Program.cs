using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Product Type (M/V/C/D):");
        char product = Convert.ToChar(Console.ReadLine().ToUpper());

        Console.WriteLine("Enter Amount:");
        double amount = Convert.ToDouble(Console.ReadLine());

        double vat = 0;

        switch (product)
        {
            case 'M':
                vat = 5;
                break;
            case 'V':
                vat = 12;
                break;
            case 'C':
                vat = 6.25;
                break;
            case 'D':
                vat = 6;
                break;
            default:
                Console.WriteLine("Invalid Product");
                return;
        }

        double vatAmount = (amount * vat) / 100;
        double total = amount + vatAmount;

        Console.WriteLine("VAT Amount: " + vatAmount);
        Console.WriteLine("Total Price: " + total);
    }
}
