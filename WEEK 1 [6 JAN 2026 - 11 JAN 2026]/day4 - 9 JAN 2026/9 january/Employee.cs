using System;
class Employee
{
    public int EmpID, Eage; public string Ename, Eaddress;
    public void GetEmpData()
    {
        Console.Write("Enter the Employee Details:-");
        this.EmpID = Convert.ToInt32(Console.ReadLine());
        this.Ename = Console.ReadLine();
        this.Eaddress = Console.ReadLine();
        this.Eage = Convert.ToInt32(Console.ReadLine());

    }
    public void DisplayEmpData()
    {
        Console.WriteLine("Employee id is : " + this.EmpID);
        Console.WriteLine("Employee name is : " + Ename);
        Console.WriteLine("Employe age is : " + Eage);
        Console.WriteLine("Employee address is : " + Eaddress);
    }
}
class classexample
{
    static void Main()
    {
        Employee emp1 = new Employee();
        Employee emp2 = new Employee();

        emp1.GetEmpData();
        emp2.GetEmpData();
        emp1.DisplayEmpData();
        emp2.DisplayEmpData();
    }
}

