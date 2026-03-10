using System;

class SortArray
{
    static void Main()
    {
        int[] a = { 1, 2, 3, 4, 5 };
        int[] b = { 9, 8, 7, 6, 5 };
        int[] output = new int[a.Length];

        Array.Sort(a);
        Array.Sort(b);
        Array.Reverse(b);

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] < 0 || b[i] < 0)
            {
                output[0] = -1;
                break;
            }
            output[i] = a[i] * b[i];
        }

        foreach (int x in output)
            Console.Write(x + " ");
    }
}
