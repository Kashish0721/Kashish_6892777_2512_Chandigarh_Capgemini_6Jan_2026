using System;

class BankAccount
{
    public string Name;
    public double Balance;

    public void Deposit(double amt)
    {
        Balance += amt;
    }

    public virtual void Show()
    {
        Console.WriteLine("Name: " + Name + " Balance: " + Balance);
    }
}

class SavingsAccount : BankAccount
{
    public void AddInterest()
    {
        Balance += Balance * 0.05;
    }

    public override void Show()
    {
        Console.WriteLine("Savings -> Name: " + Name + " Balance: " + Balance);
    }
}

class CheckingAccount : BankAccount
{
    public override void Show()
    {
        Console.WriteLine("Checking -> Name: " + Name + " Balance: " + Balance);
    }
}
