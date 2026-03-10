using System;

class CorrectChoice
{
    static void Main()
    { 
        string opt1 = (" int 1x = 10;\n");
        string opt2 = (" int x = 10;\n");
       string opt3 = (" float x = 10.0;\n");
        string opt4 =(" string x =10;\n");
        Console.WriteLine("what is the correct way to declare a variable to store an integer value in C#?\n");
        Console.WriteLine("a. " + opt1);
        Console.WriteLine("b. " + opt2);
        Console.WriteLine("c. " + opt3);
        Console.WriteLine("d. " + opt4);
        Console.WriteLine("\nEnter your Choice");
        string choice = Console.ReadLine();

        if (choice == "b" || choice =="B")
        {
            Console.WriteLine("Correct Choice");
        }
        else if(choice == "c" ||choice =="C" || choice =="d" ||choice =="D" || choice =="a" || choice == "A")
        {
            Console.WriteLine("Incorrect Choice");
        }
        else
        {
            Console.WriteLine("Invalid Answer");
        }
    }
}