using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string input1 = "AAAAA12345";
        string input2 = "AAAAA23456";
        int ratePerUnit = 4;

        int reading1 = int.Parse(new String(input1.Where(char.IsDigit).ToArray()));
        int reading2 = int.Parse(new String(input2.Where(char.IsDigit).ToArray()));

        int unitsConsumed = Math.Abs(reading2 - reading1);

        int rate = unitsConsumed * ratePerUnit;

        Console.WriteLine("the total electricity bill is : " + rate);
    }
}