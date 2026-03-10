using System;
class CountMultipleof3
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6 };
        int size = 6;
        int output = 0;

        if (size < 0)
        {
            output = -2;
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                if (arr[i] < 0)
                {
                    output = -1;
                    break;
                }
                if (arr[i] % 3 == 0)
                    output++;
            }
        }

        Console.WriteLine("Output = " + output);
        Console.ReadLine();
    }
}
