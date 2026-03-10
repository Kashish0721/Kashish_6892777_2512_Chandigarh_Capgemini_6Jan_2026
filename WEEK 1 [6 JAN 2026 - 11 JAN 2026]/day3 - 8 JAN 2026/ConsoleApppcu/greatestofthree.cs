using System;
class greatestofthree
{
    static void Main()
    {
        int a, b, c;

        Console.Write("Enter first number: ");
        a = int.Parse(Console.ReadLine());

        Console.Write("Enter Second number: ");
        b = int.Parse(Console.ReadLine());

        Console.Write("Enter Third number: ");
        c = int.Parse(Console.ReadLine());


        if (a >= b && a >= c)
        {
            Console.WriteLine("Greatest value is: " + a);
        }
        else if (b >= c && b >= a)
        {
            Console.WriteLine("Greatest value is: " + b);
        }
        else { Console.WriteLine("Greatesr value is:" + c); }
    }
}
