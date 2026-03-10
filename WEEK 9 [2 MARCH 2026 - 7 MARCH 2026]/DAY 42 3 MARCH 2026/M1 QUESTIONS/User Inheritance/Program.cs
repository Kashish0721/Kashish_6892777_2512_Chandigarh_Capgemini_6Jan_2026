using System;

namespace Solution
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public abstract class User
    {
        protected string type;
        protected string name;
        protected Gender gender;
        protected int age;

        public User(string type, string name, Gender gender, int age)
        {
            this.type = type;
            this.name = name;
            this.gender = gender;
            this.age = age;
        }

        public string GetUserName()
        {
            return name;
        }

        public string GetUserType()
        {
            return type;
        }

        public int GetAge()
        {
            return age;
        }

        public Gender GetGender()
        {
            return gender;
        }
    }

    class Admin : User
    {
        public Admin(string name, Gender gender, int age)
            : base("Admin", name, gender, age)
        {
        }
    }

    class Moderator : User
    {
        public Moderator(string name, Gender gender, int age)
            : base("Moderator", name, gender, age)
        {
        }
    }

    class Program
    {
        static void Main()
        {
            // Admin input
            string adminInput = Console.ReadLine();
            string[] adminData = adminInput.Split(' ');

            string adminName = adminData[0];
            Gender adminGender = (Gender)Enum.Parse(typeof(Gender), adminData[1]);
            int adminAge = Convert.ToInt32(adminData[2]);

            Admin admin = new Admin(adminName, adminGender, adminAge);

            Console.WriteLine($"Type of user {admin.GetUserName()} is {admin.GetUserType()}");
            Console.WriteLine($"Age of user {admin.GetUserName()} is {admin.GetAge()}");
            Console.WriteLine($"Gender of user {admin.GetUserName()} is {admin.GetGender()}");

            // Moderator input
            string modInput = Console.ReadLine();
            string[] modData = modInput.Split(' ');

            string modName = modData[0];
            Gender modGender = (Gender)Enum.Parse(typeof(Gender), modData[1]);
            int modAge = Convert.ToInt32(modData[2]);

            Moderator moderator = new Moderator(modName, modGender, modAge);

            Console.WriteLine($"Type of user {moderator.GetUserName()} is {moderator.GetUserType()}");
            Console.WriteLine($"Age of user {moderator.GetUserName()} is {moderator.GetAge()}");
            Console.WriteLine($"Gender of user {moderator.GetUserName()} is {moderator.GetGender()}");
        }
    }
}