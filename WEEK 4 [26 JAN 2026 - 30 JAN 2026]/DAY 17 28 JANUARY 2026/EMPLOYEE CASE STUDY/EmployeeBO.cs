using System.Collections.Generic;
using System;
using System.Linq;

 class EmployeeBO
{
    public void DisplayEmployeeDetails(List<Employee> employeeList, string name)
    {
        List<Employee> p1 = (from p in employeeList
                             where p.Name == name
                             select p).ToList();
        int le = p1.Count;

        if (le < 0)
        {
            Console.Write("Employee named {0} not found", name); ;
        }
        else
        {

            Console.WriteLine("Name                 Age   Designation          City");
            foreach (Employee x1 in p1)
            {
                Console.WriteLine(x1.ToString());
            }

        }

    }

    public void DisplayYoungestEmployeeDetails(List<Employee> employeeList)
    {
        /*FILL CODE HERE*/
        int age = (from p in employeeList
                   select p.Age).Min();
        var x = from p in employeeList
                where p.Age == age
                select p;

        Console.WriteLine("Name                 Age   Designation          City");
        foreach (var x1 in x)
        {
            Console.WriteLine(x1.ToString());
        }

    }

    public void displayEmployeesFromCity(List<Employee> employeeList, string cName)
    {

        /*FILL CODE HERE*/
        List<Employee> p1 = (from p in employeeList
                             where p.City == cName
                             select p).ToList();
        int le = p1.Count;

        if (le < 0)
        {
            Console.Write("Employee named {0} not found", cName); ;
        }
        else
        {

            Console.WriteLine("Name                 Age   Designation          City");
            foreach (Employee x1 in p1)
            {
                Console.WriteLine(x1.ToString());
            }

        }

    }

}
