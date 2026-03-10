using System;

class Program
{
    static void Main()
    {
        SavingsAccount s = new SavingsAccount();
        s.Name = "Amit";
        s.Balance = 1000;

        s.Deposit(500);
        s.AddInterest();
        s.Show();

        CheckingAccount c = new CheckingAccount();
        c.Name = "Ravi";
        c.Balance = 2000;
        c.Show();
    }
}
