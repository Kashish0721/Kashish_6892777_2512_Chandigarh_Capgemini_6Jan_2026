
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCU
{
    internal class Department
    {
        public int Dept_ID;
        public string DepartmentName;
        public string DepartmentHead;

        public void GetData()
        {
            Console.Write("Enter Department ID: ");
            Dept_ID = int.Parse(Console.ReadLine());

            Console.Write("Enter Department Name: ");
            DepartmentName = Console.ReadLine();

            Console.Write("Enter Department Head: ");
            DepartmentHead = Console.ReadLine();
        }

        public void CheckDepartmentType()
        {
            if (Dept_ID > 100)
            {
                Console.WriteLine("This is a Senior Department");
            }
            else
            {
                Console.WriteLine("This is a Junior Department");
            }
        }

        public void Display()
        {
            Console.WriteLine("\n--- Department Details ---");
            Console.WriteLine("Department ID   : " + Dept_ID);
            Console.WriteLine("Department Name : " + DepartmentName);
            Console.WriteLine("Department Head : " + DepartmentHead);
        }
        static void Main(string[] args)
        {
            Department dept = new Department();

            dept.GetData();
            dept.CheckDepartmentType();
            dept.Display();

            Console.ReadLine();
        }
    }
}