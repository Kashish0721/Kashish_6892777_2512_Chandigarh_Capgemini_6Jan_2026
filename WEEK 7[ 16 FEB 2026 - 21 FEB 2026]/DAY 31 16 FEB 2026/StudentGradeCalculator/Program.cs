using System;

class Program
{
    static void Main(string[] args)
    {
        GradeCalculator calculator = new GradeCalculator();

        Console.WriteLine("Enter student score:");

        int score = Convert.ToInt32(Console.ReadLine());

        string grade = calculator.GetGrade(score);

        Console.WriteLine($"Grade: {grade}");
    }
}