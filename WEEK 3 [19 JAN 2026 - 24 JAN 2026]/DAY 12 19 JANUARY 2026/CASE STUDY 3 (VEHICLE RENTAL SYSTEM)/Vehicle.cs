using System;

class Vehicle
{
    public string Name;
    public int RentPerDay;

    public virtual void Show()
    {
        Console.WriteLine("Vehicle: " + Name + " Rent/Day: " + RentPerDay);
    }
}

class Car : Vehicle
{
    public override void Show()
    {
        Console.WriteLine("Car: " + Name + " Rent/Day: " + RentPerDay);
    }
}

class Bike : Vehicle
{
    public override void Show()
    {
        Console.WriteLine("Bike : " + Name + " Rent/Day: " + RentPerDay);
    }
}

class Truck : Vehicle
{
    public override void Show()
    {
        Console.WriteLine("Truck: " + Name + " Rent/Day: " + RentPerDay);
    }
}
