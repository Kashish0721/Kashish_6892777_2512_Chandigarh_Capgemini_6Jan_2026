using System;
class Swap
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
        if (size % 2 == 0)
        {
            Console.WriteLine("-3");
            return;
        }

        int[] arr = new int[size];
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
            if (arr[i] < 0)
            {
                Console.WriteLine("-1");
                return;
            }
        }

     
        int[] outArr = new int[size];
        for (int i = 0; i < size; i++)
            outArr[i] = arr[i];

        int temp = outArr[0];
        outArr[0] = outArr[size - 1];
        outArr[size - 1] = temp;

        for (int i = 0; i < size; i++)
            Console.Write(outArr[i] + " ");

        Console.ReadLine();
    }
}
