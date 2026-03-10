using System;
using BankAccountApp;

class Program
{
    static void Main(string[] args)
    {
        var account = new BankAccount(100);

        account.Deposit(50);
        account.Withdraw(30);

        Console.WriteLine($"Final Balance: {account.Balance}");
        Console.ReadLine();
    }
}
