using System;

class Inheritance
{
    public string Name;
}

class TechDepartment : Inheritance
{
    public void Show()
    {
        Console.WriteLine("Tech Department: " + Name);
    }
}

class App
{
    static void Main()
    {
        TechDepartment t = new TechDepartment();
        t.Name = "CSE";
        t.Show();
        Console.ReadLine();
    }
}
