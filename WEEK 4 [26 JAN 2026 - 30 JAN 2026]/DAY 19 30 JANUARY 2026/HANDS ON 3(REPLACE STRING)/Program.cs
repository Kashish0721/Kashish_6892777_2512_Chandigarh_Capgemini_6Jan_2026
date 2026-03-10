using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a sentence:");
        string input1 = Console.ReadLine();

        Console.WriteLine("Enter word number to replace:");
        int input2 = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter special character:");
        char input3 = Console.ReadLine()[0];

        string result = UserProgramCode.replaceString(input1, input2, input3);

        if (result == "-1")
            Console.WriteLine("Invalid String");
        else if (result == "-2")
            Console.WriteLine("Number not positive");
        else if (result == "-3")
            Console.WriteLine("Character not a special character");
        else
            Console.WriteLine(result);
    }
}
