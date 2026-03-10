using System;

interface Inter1
{
    void Show();
}

class Inter2: Inter1
{
    public void Show()
    {
        Console.WriteLine("HR Department");
    }
}

class App3
{
    static void Main()
    {
        Inter1 d = new Inter2();
        d.Show();
        Console.ReadLine();
    }
}
