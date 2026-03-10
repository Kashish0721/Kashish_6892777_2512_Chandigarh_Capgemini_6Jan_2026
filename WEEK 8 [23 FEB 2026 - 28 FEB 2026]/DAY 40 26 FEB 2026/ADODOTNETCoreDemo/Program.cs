using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        
    
        static void Main(string[] args)
        {
            try
            {
                string connectionString =
                       "Data Source=DESKTOP-4EDFUSD\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True;TrustServerCertificate=True";
                GetAllEmployees(connectionString);

                GetEmployeeByID(connectionString, 1);

                CreateEmployeeWithAddress(
                    connectionString,
                    "Ramesh",
                    "Sharma",
                    "ramesh@example.com",
                    "123 Patia",
                    "BBSR",
                    "India",
                    "755019"
                );

                UpdateEmployeeWithAddress(
                    connectionString,
                    3,
                    "Rakesh",
                    "Sharma",
                    "rakesh@example.com",
                    "3456 Patia",
                    "CTC",
                    "India",
                    "755024",
                    3
                );

                DeleteEmployee(connectionString, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            Console.ReadLine();
        }

        // ================= GET ALL EMPLOYEES =================
        static void GetAllEmployees(string connectionString)
        {
            Console.WriteLine("GetAllEmployees Stored Procedure Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("GetAllEmployees", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"EmployeeID: {reader["EmployeeID"]}, " +
                            $"FirstName: {reader["FirstName"]}, " +
                            $"LastName: {reader["LatName"]}, " +
                            $"Email: {reader["Email"]}"
                        );

                        Console.WriteLine(
                            $"Address: {reader["Street"]}, " +
                            $"{reader["City"]}, " +
                            $"{reader["State"]}, " +
                            $"{reader["PostalCode"]}\n"
                        );
                    }
                }
            }
        }

        // ================= GET EMPLOYEE BY ID =================
        static void GetEmployeeByID(string connectionString, int employeeID)
        {
            Console.WriteLine("GetEmployeeByID Stored Procedure Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("GetEmployeeByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine(
                            $"Employee: {reader["FirstName"]} {reader["LatName"]}, " +
                            $"Email: {reader["Email"]}"
                        );

                        Console.WriteLine(
                            $"Address: {reader["Street"]}, " +
                            $"{reader["City"]}, " +
                            $"{reader["State"]}, " +
                            $"{reader["PostalCode"]}\n"
                        );
                    }
                    else
                    {
                        Console.WriteLine("Employee Not Found\n");
                    }
                }
            }
        }

        // ================= CREATE EMPLOYEE =================
        static void CreateEmployeeWithAddress(
            string connectionString,
            string firstName,
            string latName,
            string email,
            string street,
            string city,
            string state,
            string postalCode)
        {
            Console.WriteLine("CreateEmployeeWithAddress Stored Procedure Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("CreateEmployeeWithAddress", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = latName;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
                command.Parameters.Add("@Street", SqlDbType.VarChar, 100).Value = street;
                command.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = city;
                command.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = state;
                command.Parameters.Add("@PostalCode", SqlDbType.VarChar, 20).Value = postalCode;

                connection.Open();
                command.ExecuteNonQuery();

                Console.WriteLine("Employee and Address created successfully.\n");
            }
        }

        // ================= UPDATE EMPLOYEE =================
        static void UpdateEmployeeWithAddress(
            string connectionString,
            int employeeID,
            string firstName,
            string latName,
            string email,
            string street,
            string city,
            string state,
            string postalCode,
            int AddressID)
        {
            Console.WriteLine("UpdateEmployeeWithAddress Stored Procedure Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UpdateEmployeeWithAddress", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = latName;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
                command.Parameters.Add("@Street", SqlDbType.VarChar, 100).Value = street;
                command.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = city;
                command.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = state;
                command.Parameters.Add("@PostalCode", SqlDbType.VarChar, 20).Value = postalCode;
                command.Parameters.Add("@AddressID", SqlDbType.VarChar, 20).Value = AddressID;

                connection.Open();
                command.ExecuteNonQuery();

                Console.WriteLine("Employee and Address updated successfully.\n");
            }
        }

        // ================= DELETE EMPLOYEE =================
        static void DeleteEmployee(string connectionString, int employeeID)
        {
            Console.WriteLine("DeleteEmployee Stored Procedure Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DeleteEmployee", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                    Console.WriteLine("Employee and Address deleted successfully.\n");
                else
                    Console.WriteLine("Employee not found.\n");
            }
        }
    }
}