using System;

class Department
{
    public const string College = "CU";
    public static int Count = 0;
    public readonly int Code;
    public string Name;


    public Department()
    {
        Name = "Not Set";
        Code = 100;
        Count++;
    }


    public Department(string n, int c)
    {
        Name = n;
        Code = c;
        Count++;
    }

    public void Show()
    {
        Console.WriteLine(College + " - " + Name + " - " + Code);
    }
}

class App
{
    static void Main()
    {
        Department d1 = new Department();
        d1.Show();

        Department d2 = new Department("CSE", 501);
        d2.Show();

        Console.WriteLine("Total: " + Department.Count);
        Console.ReadLine();
    }
}
