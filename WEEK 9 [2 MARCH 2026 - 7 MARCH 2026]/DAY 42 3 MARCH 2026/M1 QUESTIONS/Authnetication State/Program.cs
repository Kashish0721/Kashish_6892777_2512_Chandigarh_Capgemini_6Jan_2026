using System;
using System.Collections.Generic;
using System.Linq;

interface IUser
{
    int Id { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    int IncorrectAttempt { get; set; }
    string Location { get; set; }
}

interface IApplicationAuthState
{
    List<IUser> RegisteredUsers { get; set; }
    List<IUser> UsersLoggedIn { get; set; }
    List<string> AllowedLocations { get; set; }

    string Register(IUser user);
    string Login(IUser user);
    string Logout(IUser user);
}

class User : IUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IncorrectAttempt { get; set; }
    public string Location { get; set; }

    public User(int id, string email, string password, string location)
    {
        Id = id;
        Email = email;
        Password = password;
        Location = location;
        IncorrectAttempt = 0;
    }
}

class ApplicationAuthState : IApplicationAuthState
{
    public List<IUser> RegisteredUsers { get; set; }
    public List<IUser> UsersLoggedIn { get; set; }
    public List<string> AllowedLocations { get; set; }

    public ApplicationAuthState(List<string> allowedLocations)
    {
        AllowedLocations = allowedLocations;
        RegisteredUsers = new List<IUser>();
        UsersLoggedIn = new List<IUser>();
    }

    public string Register(IUser user)
    {
        if (RegisteredUsers.Any(u => u.Email == user.Email))
            return $"{user.Email} is already registered!";

        RegisteredUsers.Add(user);
        return $"{user.Email} registered successfully!";
    }

    public string Login(IUser user)
    {
        var regUser = RegisteredUsers.FirstOrDefault(u => u.Email == user.Email);

        if (regUser == null)
            return $"{user.Email} is not registered!";

        if (regUser.IncorrectAttempt >= 3)
            return $"{user.Email} is blocked!";

        if (!AllowedLocations.Contains(user.Location))
            return $"{user.Email} is not allowed to login from this location!";

        if (UsersLoggedIn.Any(u => u.Email == user.Email && u.Location != user.Location))
            return $"{user.Email} is already logged in from another location!";

        if (UsersLoggedIn.Any(u => u.Email == user.Email))
            return $"{user.Email} is already logged in!";

        if (regUser.Password != user.Password)
        {
            regUser.IncorrectAttempt++;
            return $"{user.Email} password is incorrect!";
        }

        regUser.IncorrectAttempt = 0;
        UsersLoggedIn.Add(regUser);

        return $"{user.Email} logged in successfully!";
    }

    public string Logout(IUser user)
    {
        var loggedUser = UsersLoggedIn.FirstOrDefault(u => u.Email == user.Email);

        if (loggedUser == null)
            return $"{user.Email} is not logged in!";

        UsersLoggedIn.Remove(loggedUser);

        return $"{user.Email} logged out successfully!";
    }
}

class Program
{
    static void Main()
    {
        List<string> allowedLocations = new List<string>();

        int allowedLocationCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < allowedLocationCount; i++)
        {
            allowedLocations.Add(Console.ReadLine());
        }

        ApplicationAuthState authState = new ApplicationAuthState(allowedLocations);

        List<IUser> users = new List<IUser>();

        int usersCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < usersCount; i++)
        {
            var a = Console.ReadLine().Split(',');

            users.Add(new User(
                Convert.ToInt32(a[0]),
                a[1],
                a[2],
                a[3]
            ));
        }

        int actionCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < actionCount; i++)
        {
            var a = Console.ReadLine().Split(':');
            int index = Convert.ToInt32(a[1]);

            if (a[0] == "Register")
                Console.WriteLine(authState.Register(users[index]));

            else if (a[0] == "Login")
                Console.WriteLine(authState.Login(users[index]));

            else if (a[0] == "Logout")
                Console.WriteLine(authState.Logout(users[index]));
        }
    }
}