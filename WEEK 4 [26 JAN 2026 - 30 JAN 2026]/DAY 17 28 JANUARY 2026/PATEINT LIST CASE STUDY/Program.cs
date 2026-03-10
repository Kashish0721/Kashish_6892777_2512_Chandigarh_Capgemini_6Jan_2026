using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Patient> patientList = new List<Patient>();

        Console.WriteLine("Enter the number of patients");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter patient " + (i + 1) + " details:");
            Console.WriteLine("Enter the name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the age");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the illness");
            string illness = Console.ReadLine();

            Console.WriteLine("Enter the city");
            string city = Console.ReadLine();

            Patient p = new Patient(name, age, illness, city);
            patientList.Add(p);
        }

        PatientBO bo = new PatientBO();
        string opt;

        do
        {
            Console.WriteLine("Enter your choice:");
            Console.WriteLine("1)Display Patient Details");
            Console.WriteLine("2)Display Youngest Patient Details");
            Console.WriteLine("3)Display Patients from City");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter patient name:");
                    string pname = Console.ReadLine();
                    bo.DisplayPatientDetails(patientList, pname);
                    break;

                case 2:
                    Console.WriteLine("The Youngest Patient Details");
                    bo.DisplayYoungestPatientDetails(patientList);
                    break;

                case 3:
                    Console.WriteLine("Enter city");
                    string cname = Console.ReadLine();
                    bo.DisplayPatientsFromCity(patientList, cname);
                    break;
            }

            Console.WriteLine("Do you want to continue(Yes/No)?");
            opt = Console.ReadLine();

        } while (opt.Equals("Yes"));

    }
}
