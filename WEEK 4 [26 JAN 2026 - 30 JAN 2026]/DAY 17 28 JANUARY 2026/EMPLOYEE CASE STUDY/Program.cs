using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Program
{
    static void Main(string[] args)
    {
        List<Employee> employeeList = new List<Employee>();
        int noOfEmployees, i;
        string name, city, designation;
        int age;

        Console.WriteLine("Enter the number of employees");
        noOfEmployees = int.Parse(Console.ReadLine());
        Employee employee = new Employee();
        for (i = 0; i < noOfEmployees; i++)
        {
            Console.WriteLine("Enter employee " + (i + 1) + " details:");
            Console.WriteLine("Enter the name");
            name = Console.ReadLine();
            Console.WriteLine("Enter the age");
            age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the designation");
            designation = Console.ReadLine();
            Console.WriteLine("Enter the city");
            city = Console.ReadLine();
            employee = new Employee(name, age, designation, city);
            employeeList.Add(employee);
        }
        int choice;
        EmployeeBO employeeBO = new EmployeeBO();
        string opt;
        do
        {
            Console.WriteLine("Enter your choice:\n1)Display Employee Details\n2)Display Youngest Employee Details");
            Console.WriteLine("3)Display Employees from City");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter employee name:");
                    name = Console.ReadLine();
                    employeeBO.DisplayEmployeeDetails(employeeList, name);
                    break;

                case 2:
                    employeeBO.DisplayYoungestEmployeeDetails(employeeList);
                    break;

                case 3:
                    Console.WriteLine("Enter city");
                    city = Console.ReadLine();
                    employeeBO.displayEmployeesFromCity(employeeList, city);
                    break;

                default:
                    break;

            }
            Console.WriteLine("Do you want to continue(Yes/No)?");
            opt = Console.ReadLine();
        } while (opt.Equals("Yes"));
    }
}

