using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    public int Id { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
    public string Roles { get; set; }
    public DateTime? JoinDate { get; set; }
    public decimal Credit { get; set; }
    public string Status { get; set; }
}

static class UserManager
{
    public static (List<User>, List<User>) CompareUsers(List<User> usersListInDB, List<User> newUsersList)
    {
        List<User> updated = new List<User>();
        List<User> inserted = new List<User>();

        foreach (var newUser in newUsersList)
        {
            if (newUser.Id == 0)
            {
                inserted.Add(newUser);
            }
            else
            {
                var existingUser = usersListInDB.FirstOrDefault(x => x.Id == newUser.Id);

                if (existingUser != null)
                {
                    if (existingUser.FirstName != newUser.FirstName ||
                        existingUser.LastName != newUser.LastName ||
                        existingUser.Email != newUser.Email ||
                        existingUser.Status != newUser.Status)
                    {
                        updated.Add(newUser);
                    }
                }
            }
        }

        return (updated, inserted);
    }
}

class Program
{
    static void Main()
    {
        List<User> usersListInDB = new List<User>();
        List<User> newUsersList = new List<User>();

        int userInDbCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < userInDbCount; i++)
        {
            var u = Console.ReadLine().Split(',');

            usersListInDB.Add(new User
            {
                Id = Convert.ToInt32(u[0]),
                FirstName = u[2],
                LastName = u[3],
                Email = u[6],
                Status = u[17]
            });
        }

        int newUsersCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < newUsersCount; i++)
        {
            var u = Console.ReadLine().Split(',');

            newUsersList.Add(new User
            {
                Id = Convert.ToInt32(u[0]),
                FirstName = u[2],
                LastName = u[3],
                Email = u[6],
                Status = u[17]
            });
        }

        var result = UserManager.CompareUsers(usersListInDB, newUsersList);

        Console.WriteLine("Updated Users:" + result.Item1.Count);
        Console.WriteLine("Inserted Users:" + result.Item2.Count);
    }
}