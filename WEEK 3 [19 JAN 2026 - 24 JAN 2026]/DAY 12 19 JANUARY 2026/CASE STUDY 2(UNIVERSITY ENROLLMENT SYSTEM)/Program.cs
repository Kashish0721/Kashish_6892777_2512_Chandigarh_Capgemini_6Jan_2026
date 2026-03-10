using System;

class Program
{
    static void Main()
    {
        Student s = new Student();
        s.Name = "Kashish";
        s.Id = 101;
        s.Show();

        Professor p = new Professor();
        p.Name = "Dr. Sharma";
        p.Subject = "C#";
        p.Show();
    }
}
