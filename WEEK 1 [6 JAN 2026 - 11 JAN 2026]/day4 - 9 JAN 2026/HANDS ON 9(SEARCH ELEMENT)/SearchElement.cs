using System;

class SearchElement
{
    static void Main()
    {
        int[] arr = { 4, 7, 9, 2 };
        int search = 9;
        int output = 1;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < 0)
            {
                output = -1;
                break;
            }
            if (arr[i] == search)
            {
                output = i;
                break;
            }
        }

        Console.WriteLine("Output: " + output);
    }
}