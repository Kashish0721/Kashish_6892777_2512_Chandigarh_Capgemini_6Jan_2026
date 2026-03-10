using System;

class grade
{
    static void Main()
    {
        int number;
        Console.WriteLine("Enter your Marks :");
        number = int.Parse(Console.ReadLine());

        if (number <= 100 && number >= 90)
        {
            Console.WriteLine("Your grade is A+");
        }
        else if (number < 90 && number >= 80)
        {
            Console.WriteLine("Your grade is A");
        }
        else if (number < 80 && number >= 70)
        {
            Console.WriteLine("Your grade is B");
        }
        else if (number < 70 && number >= 60)
        {
            Console.WriteLine("Your grade is C+");
        }
        else if (number < 60 && number >= 50)
        {
            Console.WriteLine("Your grade is C");
        }
        else
        {
            Console.WriteLine("Failed!!");
        }

    }
}