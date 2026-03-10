using System;

class Temperature
{
    static void Main()
    {
        Console.Write("Enter Fahrenheit value: ");
        int f = int.Parse(Console.ReadLine());

        int output;

        if (f < 0)
            output = -1;
        else
            output = (f - 32) * 5 / 9;

        Console.WriteLine("Temperature in Celcius: " + output);
        Console.ReadLine();
    }
}
