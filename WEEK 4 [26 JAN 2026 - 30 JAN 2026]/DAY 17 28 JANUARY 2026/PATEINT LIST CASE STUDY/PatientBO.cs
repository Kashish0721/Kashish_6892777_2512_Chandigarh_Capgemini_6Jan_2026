using System;
using System.Collections.Generic;
using System.Linq;

class PatientBO
{
    public void DisplayPatientDetails(List<Patient> patientList, string name)
    {
        List<Patient> p1 = (from p in patientList
                            where p.Name == name
                            select p).ToList();

        if (p1.Count == 0)
        {
            Console.WriteLine("Patient named {0} not found", name);
        }
        else
        {
            Console.WriteLine("Name                 Age   Illness          City");
            foreach (Patient x in p1)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }

    public void DisplayYoungestPatientDetails(List<Patient> patientList)
    {
        int minAge = (from p in patientList
                      select p.Age).Min();

        var youngest = from p in patientList
                       where p.Age == minAge
                       select p;

        Console.WriteLine("Name                 Age   Illness          City");
        foreach (var x in youngest)
        {
            Console.WriteLine(x.ToString());
        }
    }

    public void DisplayPatientsFromCity(List<Patient> patientList, string cname)
    {
        List<Patient> p1 = (from p in patientList
                            where p.City == cname
                            select p).ToList();

        if (p1.Count == 0)
        {
            Console.WriteLine("City named {0} not found", cname);
        }
        else
        {
            Console.WriteLine("Name                 Age   Illness          City");
            foreach (Patient x in p1)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
}
