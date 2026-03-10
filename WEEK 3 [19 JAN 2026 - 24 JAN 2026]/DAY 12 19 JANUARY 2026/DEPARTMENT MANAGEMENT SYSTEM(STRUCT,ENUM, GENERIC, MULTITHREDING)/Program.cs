using System;
using System.Collections.Generic;
using System.Threading;

enum DeptType   // ENUM
{
    Technical,
    HR,
    Finance
}

struct DeptInfo   // STRUCT
{
    public int Id;
    public string Name;
    public DeptType Type;
}

class Repository<T>   // GENERIC
{
    private List<T> data = new List<T>();

    public void Add(T item)
    {
        data.Add(item);
    }

    public void ShowAll()
    {
        foreach (var x in data)
            Console.WriteLine(x);
    }
}

class App
{
    static void LoadData()   // MULTI-THREAD METHOD
    {
        Console.WriteLine("Loading Departments...");
        Thread.Sleep(2000);
        Console.WriteLine("Data Loaded!");
    }

    static void Main()
    {
        Thread t = new Thread(LoadData);  // Multithreading
        t.Start();

        DeptInfo d1;
        d1.Id = 1;
        d1.Name = "CSE";
        d1.Type = DeptType.Technical;

        DeptInfo d2;
        d2.Id = 2;
        d2.Name = "HR";
        d2.Type = DeptType.HR;

        Repository<DeptInfo> repo = new Repository<DeptInfo>();
        repo.Add(d1);
        repo.Add(d2);

        t.Join();
        Console.WriteLine("Departments Stored.");
        Console.ReadLine();
    }
}
