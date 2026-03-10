using System;

// Interface 1
interface IDepartmentOps
{
    void AddDepartment();
    void UpdateDepartment();
    void DeleteDepartment();
    void ViewDepartment();
    void SearchDepartment();
}

// Interface 2
interface IReportOps
{
    void GenerateReport();
    void ShowTotalDepartments();
    void ShowHighestBudget();
    void ExportReport();
    void PrintSummary();
}

// Class implementing both interfaces
class DepartmentManager : IDepartmentOps, IReportOps
{
    public void AddDepartment()
    {
        Console.WriteLine("Department Added");
    }

    public void UpdateDepartment()
    {
        Console.WriteLine("Department Updated");
    }

    public void DeleteDepartment()
    {
        Console.WriteLine("Department Deleted");
    }

    public void ViewDepartment()
    {
        Console.WriteLine("Viewing Department");
    }

    public void SearchDepartment()
    {
        Console.WriteLine("Searching Department");
    }

    public void GenerateReport()
    {
        Console.WriteLine("Report Generated");
    }

    public void ShowTotalDepartments()
    {
        Console.WriteLine("Total Departments: 5");
    }

    public void ShowHighestBudget()
    {
        Console.WriteLine("Highest Budget: CSE");
    }

    public void ExportReport()
    {
        Console.WriteLine("Report Exported");
    }

    public void PrintSummary()
    {
        Console.WriteLine("Summary Printed");
    }
}

class Program
{
    static void Main()
    {
        DepartmentManager dm = new DepartmentManager();

        dm.AddDepartment();
        dm.ViewDepartment();
        dm.GenerateReport();
        dm.ShowTotalDepartments();
        dm.PrintSummary();

        Console.ReadLine();
    }
}
