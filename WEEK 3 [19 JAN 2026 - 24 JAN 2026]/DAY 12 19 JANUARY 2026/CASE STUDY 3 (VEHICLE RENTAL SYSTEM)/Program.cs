using System;
using System.Runtime.ConstrainedExecution;

class Program
{
    static void Main()
    {
        Car c = new Car();
        c.Name = "Honda City";
        c.RentPerDay = 1500;
        c.Show();

        Bike b = new Bike();
        b.Name = "Pulsar";
        b.RentPerDay = 500;
        b.Show();
    }
}
