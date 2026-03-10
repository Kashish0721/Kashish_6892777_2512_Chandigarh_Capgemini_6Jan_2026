using System;

abstract class  Abstraction
{
    public abstract void Work();
}

class Tech : Abstraction
{
    public override void Work()
    {
        Console.WriteLine("Technical Work");
    }
}

class App2
{
    static void Main()
    {
        Abstraction d = new Tech();
        d.Work();
        Console.ReadLine();
    }
}
