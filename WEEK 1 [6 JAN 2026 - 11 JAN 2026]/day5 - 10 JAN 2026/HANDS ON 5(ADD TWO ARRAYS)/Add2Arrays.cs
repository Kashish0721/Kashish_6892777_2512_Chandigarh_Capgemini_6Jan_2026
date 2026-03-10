using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int size = int.Parse(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("-2");
            return;
        }

        int[] a = new int[size];
        int[] b = new int[size];

        Console.WriteLine("Enter array1:");
        for (int i = 0; i < size; i++)
        {
            a[i] = int.Parse(Console.ReadLine());
            if (a[i] < 0) { Console.WriteLine("-1"); return; }
        }

        Console.WriteLine("Enter array2:");
        for (int i = 0; i < size; i++)
        {
            b[i] = int.Parse(Console.ReadLine());
            if (b[i] < 0) { Console.WriteLine("-1"); return; }
        }

        for (int i = 0; i < size; i++)
            Console.Write((a[i] + b[i]) + " ");

        Console.ReadLine();
    }
}
