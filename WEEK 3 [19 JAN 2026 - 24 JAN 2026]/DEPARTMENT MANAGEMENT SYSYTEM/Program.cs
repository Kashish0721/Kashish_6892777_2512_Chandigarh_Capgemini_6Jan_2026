using System;
using System.Collections.Generic;
using System.Threading;

//ENUM
enum DeptType
{
    Technical,
    HR,
    Finance
}

// STRUCT
struct DeptMeta
{
    public int CreatedYear;
    public string Location;
}

// INTERFACES
interface IDepartmentOps
{
    void Add();
    void View();
    void Delete(int id);
    void Search(int id);
    void Summary();
}

interface IReportOps
{
    void Generate();
    void Export();
    void Print();
    void ShowHighest();
    void ShowTotal();
}

// ABSTRACT CLASS
abstract class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DeptType Type { get; set; }
    public DeptMeta Meta;

    public abstract double CalculateBudget();

    public virtual void Show()
    {
        Console.WriteLine($"ID: {Id}, Name: {Name}, Type: {Type}");
        Console.WriteLine($"Year: {Meta.CreatedYear}, Location: {Meta.Location}");
    }
}

//  INHERITANCE
class TechnicalDepartment : Department
{
    public int Engineers;
    public override double CalculateBudget() => Engineers * 50000;

    public override void Show()
    {
        base.Show();
        Console.WriteLine("Engineers: " + Engineers);
        Console.WriteLine("Budget: " + CalculateBudget());
    }
}

sealed class HRDepartment : Department   // SEALED
{
    public int Recruiters;
    public override double CalculateBudget() => Recruiters * 30000;

    public override void Show()
    {
        base.Show();
        Console.WriteLine("Recruiters: " + Recruiters);
        Console.WriteLine("Budget: " + CalculateBudget());
    }
}

// GENERIC REPOSITORY
class Repository<T>
{
    private List<T> data = new List<T>();
    public void Add(T item) => data.Add(item);
    public void Remove(T item) => data.Remove(item);
    public List<T> All() => data;
}

//EVENTS & DELEGATES
class DeptNotifier
{
    public delegate void DeptHandler(string msg);
    public event DeptHandler OnChange;

    public void Notify(string msg)
    {
        OnChange?.Invoke(msg);
    }
}

//PARTIAL CLASS
partial class Manager { }
partial class Manager : IDepartmentOps, IReportOps
{
    Repository<Department> repo = new Repository<Department>();
    DeptNotifier notifier = new DeptNotifier();
    int autoId = 1;

    public Manager()
    {
        notifier.OnChange += (m) => Console.WriteLine("[EVENT] " + m);
    }

    public void Add()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.WriteLine("1. Technical  2. HR");
        int t = int.Parse(Console.ReadLine());

        Department d;
        if (t == 1)
        {
            Console.Write("Engineers: ");
            int e = int.Parse(Console.ReadLine());
            d = new TechnicalDepartment { Engineers = e, Type = DeptType.Technical };
        }
        else
        {
            Console.Write("Recruiters: ");
            int r = int.Parse(Console.ReadLine());
            d = new HRDepartment { Recruiters = r, Type = DeptType.HR };
        }

        d.Id = autoId++;
        d.Name = name;
        d.Meta = new DeptMeta { CreatedYear = DateTime.Now.Year, Location = "India" };

        repo.Add(d);
        notifier.Notify("Department Added");
    }

    public void View()
    {
        foreach (var d in repo.All())
        {
            d.Show();   // Polymorphism
            Console.WriteLine("-------------");
        }
    }

    public void Delete(int id)
    {
        var d = repo.All().Find(x => x.Id == id);
        if (d != null)
        {
            repo.Remove(d);
            notifier.Notify("Department Deleted");
        }
    }

    public void Search(int id)
    {
        var d = repo.All().Find(x => x.Id == id);
        if (d != null) d.Show();
        else Console.WriteLine("Not Found");
    }

    public void Summary()
    {
        Console.WriteLine("Total: " + repo.All().Count);
    }

    public void Generate() { Console.WriteLine("Report Generated"); }
    public void Export() { Console.WriteLine("Exported"); }
    public void Print() { Console.WriteLine("Printed"); }
    public void ShowHighest() { Console.WriteLine("Highest Budget Feature"); }
    public void ShowTotal() { Summary(); }
}

//MULTITHREAD LOADER 
class Loader
{
    public static void Load()
    {
        Console.WriteLine("Loading System...");
        Thread.Sleep(1500);
        Console.WriteLine("Ready!");
    }
}

// MAIN
class App
{
    static void Main()
    {
        Thread t = new Thread(Loader.Load);
        t.Start();
        t.Join();

        Manager m = new Manager();

        while (true)
        {
            Console.WriteLine("\n1.Add 2.View 3.Search 4.Delete 5.Summary 6.Exit");
            int ch = int.Parse(Console.ReadLine());

            if (ch == 1) m.Add();
            else if (ch == 2) m.View();
            else if (ch == 3) { Console.Write("ID: "); m.Search(int.Parse(Console.ReadLine())); }
            else if (ch == 4) { Console.Write("ID: "); m.Delete(int.Parse(Console.ReadLine())); }
            else if (ch == 5) m.Summary();
            else break;
        }
    }
}
