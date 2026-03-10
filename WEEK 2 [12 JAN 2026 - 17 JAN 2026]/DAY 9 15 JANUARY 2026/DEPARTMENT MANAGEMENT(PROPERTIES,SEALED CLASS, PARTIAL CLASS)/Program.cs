using System;

// PARTIAL CLASS
partial class Department
{
    private int id;
    private string name;

    // Properties
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}

// PARTIAL CLASS
partial class Department
{
    public void Show()
    {
        Console.WriteLine("ID   : " + Id);
        Console.WriteLine("Name : " + Name);
    }
}

// SEALED CLASS 
sealed class HRDepartment : Department
{
    public string Head { get; set; }   // auto-property

    public void DisplayHR()
    {
        Show();
        Console.WriteLine("Head : " + Head);
    }
}

class Program
{
    static void Main()
    {
        HRDepartment hr = new HRDepartment();
        hr.Id = 101;               // Property use
        hr.Name = "HR";
        hr.Head = "Dr. Sharma";

        hr.DisplayHR();

        Console.ReadLine();
    }
}
