using System;

class Product
{
    public string Name;
    public int Price;

    public virtual void Show()
    {
        Console.WriteLine("Product: " + Name + " Price: " + Price);
    }
}

class Electronics : Product
{
    public override void Show()
    {
        Console.WriteLine("Electronics: " + Name + " Price: " + Price);
    }
}

class Clothing : Product
{
    public override void Show()
    {
        Console.WriteLine("Clothing: " + Name + " Price: " + Price);
    }
}

class Books : Product
{
    public override void Show()
    {
        Console.WriteLine("Book: " + Name + " Price: " + Price);
    }
}
