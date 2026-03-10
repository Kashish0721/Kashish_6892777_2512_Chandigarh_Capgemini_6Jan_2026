using System;

class CountCurrency
{
    static void Main()
    {
        Console.Write("Enter amount in rupees: ");
        int amt = int.Parse(Console.ReadLine());

        if (amt < 0)
        {
            Console.WriteLine("Output = -1");
            Console.ReadLine();
            return;
        }

        int count = 0;

        int n500 = amt / 500;
        amt = amt % 500;

        int n100 = amt / 100;
        amt = amt % 100;

        int n50 = amt / 50;
        amt = amt % 50;

        int n10 = amt / 10;
        amt = amt % 10;

        int n1 = amt;

        count = n500 + n100 + n50 + n10 + n1;

        Console.WriteLine("500 - " + n500);
        Console.WriteLine("100 - " + n100);
        Console.WriteLine("50  - " + n50);
        Console.WriteLine("10  - " + n10);
        Console.WriteLine("1   - " + n1);

        Console.WriteLine("Output = " + count);

        Console.ReadLine();
    }
}
