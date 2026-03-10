using System;
using System.Collections.Generic;

// -------- ENUM --------
enum Role { Student, Teacher, Admin }

// -------- ABSTRACT CLASS (Abstraction) --------
abstract class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public abstract void ShowMenu();
}

// -------- INTERFACES --------
interface IStudentOps
{
    void ViewProfile();
    void ViewMarks();
}

interface ITeacherOps
{
    void AddMarks();
    void ViewStudents();
}

interface IAdminOps
{
    void AddUser();
    void ViewAll();
}

// -------- INHERITANCE --------
class Student : User, IStudentOps
{
    public int Marks { get; set; }

    public override void ShowMenu()
    {
        Console.WriteLine("1. View Profile\n2. View Marks");
        int ch = int.Parse(Console.ReadLine());
        if (ch == 1) ViewProfile();
        else ViewMarks();
    }

    public void ViewProfile()
    {
        Console.WriteLine($"Student: {Name}, ID: {Id}");
    }

    public void ViewMarks()
    {
        Console.WriteLine("Marks: " + Marks);
    }
}

class Teacher : User, ITeacherOps
{
    public override void ShowMenu()
    {
        Console.WriteLine("1. Add Marks\n2. View Students");
        int ch = int.Parse(Console.ReadLine());
        if (ch == 1) AddMarks();
        else ViewStudents();
    }

    public void AddMarks()
    {
        Console.WriteLine("Marks Added");
    }

    public void ViewStudents()
    {
        Console.WriteLine("Showing Students (Demo)");
    }
}

sealed class Admin : User, IAdminOps   // sealed
{
    static List<User> users;

    public Admin(List<User> u) { users = u; }

    public override void ShowMenu()
    {
        Console.WriteLine("1. Add User\n2. View All Users");
        int ch = int.Parse(Console.ReadLine());
        if (ch == 1) AddUser();
        else ViewAll();
    }

    public void AddUser()
    {
        Console.Write("Enter Name: ");
        string n = Console.ReadLine();
        users.Add(new Student { Id = users.Count + 1, Name = n, Marks = 0 });
        Console.WriteLine("Student Added");
    }

    public void ViewAll()
    {
        foreach (var u in users)
            Console.WriteLine($"{u.Id} - {u.Name} - {u.GetType().Name}");
    }
}

//MAIN
class App
{
    static void Main()
    {
        List<User> users = new List<User>();

        users.Add(new Student { Id = 1, Name = "Aman", Marks = 85 });
        users.Add(new Teacher { Id = 2, Name = "Mr. Sharma" });

        Console.WriteLine("Login As: 1.Student  2.Teacher  3.Admin");
        int r = int.Parse(Console.ReadLine());

        User u;
        if (r == 1) u = users[0];
        else if (r == 2) u = users[1];
        else u = new Admin(users) { Id = 99, Name = "Admin" };

        u.ShowMenu();   // Polymorphism
        Console.ReadLine();
    }
}

