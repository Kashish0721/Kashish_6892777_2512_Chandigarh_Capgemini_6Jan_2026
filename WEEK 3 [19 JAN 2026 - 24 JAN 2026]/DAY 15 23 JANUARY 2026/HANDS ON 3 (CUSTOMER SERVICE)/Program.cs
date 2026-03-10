using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Queue<string> tickets = new Queue<string>();
        tickets.Enqueue("Login Issue");
        tickets.Enqueue("Payment Failed");
        tickets.Enqueue("App Crash");

        Stack<string> actions = new Stack<string>();

        // Process three tickets
        for (int i = 0; i < 3; i++)
        {
            string t = tickets.Dequeue();
            Console.WriteLine("Processing: " + t);
            actions.Push("Handled " + t);
        }

        // Undo last action
        Console.WriteLine("Undo: " + actions.Pop());

        Console.WriteLine("\nRemaining Queue:");
        foreach (var t in tickets)
            Console.WriteLine(t);
    }
}
