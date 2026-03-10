using System;

class GreaterElement
{
    static void Main()
    {
        int[] a = { 3, 6, 1 };
        int[] b = { 2, 8, 4 };
        int[] output = new int[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] < 0 || b[i] < 0)
            {
                output[0] = -1;
                break;
            }
            output[i] = (a[i] > b[i]) ? a[i] : b[i];
        }

        foreach (int x in output)
            Console.Write(x + " ");
    }
}