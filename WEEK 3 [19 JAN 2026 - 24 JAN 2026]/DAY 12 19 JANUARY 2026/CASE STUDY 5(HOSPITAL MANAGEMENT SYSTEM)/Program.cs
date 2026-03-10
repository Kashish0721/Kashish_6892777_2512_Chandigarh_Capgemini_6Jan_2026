using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        Patient p = new Patient();
        p.Name = "Rahul";
        p.Disease = "Fever";
        p.Show();

        Doctor d = new Doctor();
        d.Name = "Dr. Mehta";
        d.Specialization = "Cardiology";
        d.Show();
    }
}
