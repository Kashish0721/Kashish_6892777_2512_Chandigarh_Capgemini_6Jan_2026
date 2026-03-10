using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static string conStr =
        @"Server=DESKTOP-4EDFUSD\SQLEXPRESS;Database=UniversityDB;Trusted_Connection=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1.Insert 2.View 3.Update 4.Delete 5.Exit");
            int ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1: Insert(); break;
                case 2: View(); break;
                case 3: Update(); break;
                case 4: Delete(); break;
                case 5: return;
            }
        }
    }

    static void Insert()
    {
        using SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("sp_InsertStudent", con);
        cmd.CommandType = CommandType.StoredProcedure;

        Console.Write("First Name: "); cmd.Parameters.AddWithValue("@FirstName", Console.ReadLine());
        Console.Write("Last Name: "); cmd.Parameters.AddWithValue("@LastName", Console.ReadLine());
        Console.Write("Email: "); cmd.Parameters.AddWithValue("@Email", Console.ReadLine());
        Console.Write("DeptId: "); cmd.Parameters.AddWithValue("@DeptId", int.Parse(Console.ReadLine()));

        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Inserted");
    }

    static void View()
    {
        using SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("sp_GetStudents", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
            Console.WriteLine($"{dr[0]} {dr[1]} {dr[2]} {dr[3]} {dr[4]}");
    }

    static void Update()
    {
        using SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("sp_UpdateStudent", con);
        cmd.CommandType = CommandType.StoredProcedure;

        Console.Write("Id: "); cmd.Parameters.AddWithValue("@StudentId", int.Parse(Console.ReadLine()));
        Console.Write("First Name: "); cmd.Parameters.AddWithValue("@FirstName", Console.ReadLine());
        Console.Write("Last Name: "); cmd.Parameters.AddWithValue("@LastName", Console.ReadLine());
        Console.Write("Email: "); cmd.Parameters.AddWithValue("@Email", Console.ReadLine());
        Console.Write("DeptId: "); cmd.Parameters.AddWithValue("@DeptId", int.Parse(Console.ReadLine()));

        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Updated");
    }

    static void Delete()
    {
        using SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("sp_DeleteStudent", con);
        cmd.CommandType = CommandType.StoredProcedure;

        Console.Write("Id: "); cmd.Parameters.AddWithValue("@StudentId", int.Parse(Console.ReadLine()));
        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Deleted");
    }
}