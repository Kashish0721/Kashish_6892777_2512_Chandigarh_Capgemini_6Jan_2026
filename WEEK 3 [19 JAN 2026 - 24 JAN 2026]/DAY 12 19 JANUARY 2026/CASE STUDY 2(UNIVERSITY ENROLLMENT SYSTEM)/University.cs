using System;

class Person
{
    public string Name;

    public virtual void Show()
    {
        Console.WriteLine("Name: " + Name);
    }
}

class Student : Person
{
    public int Id;

    public override void Show()
    {
        Console.WriteLine("Student -> Name: " + Name + " ID: " + Id);
    }
}

class Professor : Person
{
    public string Subject;

    public override void Show()
    {
        Console.WriteLine("Professor -> Name: " + Name + " Subject: " + Subject);
    }
}

class Staff : Person
{
    public string Role;

    public override void Show()
    {
        Console.WriteLine("Staff -> Name: " + Name + " Role: " + Role);
    }
}
