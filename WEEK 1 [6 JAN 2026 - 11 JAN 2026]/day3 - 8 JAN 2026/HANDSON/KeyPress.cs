using System;

class KeyPress
{
    static void Main()
    {
        Console.WriteLine("Press a number (0–9):");

        string input = Console.ReadLine();

        if (input == "0")
            Console.WriteLine("Number pressed: 0");
        else if (input == "1")
            Console.WriteLine("Number pressed: 1");
        else if (input == "2")
            Console.WriteLine("Number pressed: 2");
        else if (input == "3")
            Console.WriteLine("Number pressed: 3");
        else if (input == "4")
            Console.WriteLine("Number pressed: 4");
        else if (input == "5")
            Console.WriteLine("Number pressed: 5");
        else if (input == "6")
            Console.WriteLine("Number pressed: 6");
        else if (input == "7")
            Console.WriteLine("Number pressed: 7");
        else if (input == "8")
            Console.WriteLine("Number pressed: 8");
        else if (input == "9")
            Console.WriteLine("Number pressed: 9");
        else
            Console.WriteLine("Not allowed");
    }
}


