using System;

class Polymorphism
{
    public virtual void Show()
    {
        Console.WriteLine("Base Department");
    }
}

class HR : Polymorphism
{
    public override void Show()
    {
        Console.WriteLine("HR Department");
    }
}

class App1
{
    static void Main()
    {
        Polymorphism d = new HR();
        d.Show();
        Console.ReadLine();
    }
}
