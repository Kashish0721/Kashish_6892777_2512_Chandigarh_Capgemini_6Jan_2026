using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static string conStr =
@"Server=DESKTOP-4EDFUSD\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        using SqlConnection con = new SqlConnection(conStr);
        SqlDataAdapter da = new SqlDataAdapter("sp_GetBooks", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;

        DataSet ds = new DataSet();
          da.Fill(ds, "Books");

        foreach (DataRow row in ds.Tables["Books"].Rows)
            Console.WriteLine($"{row["BookId"]} {row["Title"]}");
    }
}