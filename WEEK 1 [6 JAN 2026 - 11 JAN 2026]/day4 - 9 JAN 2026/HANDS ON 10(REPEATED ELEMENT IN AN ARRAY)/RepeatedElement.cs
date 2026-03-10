using System;

class RepeatedElement
{
    static void Main()
    {
        int[] arr = { 2, 2, 2, 2, 3, 3, 3, 3, 4 };

        for (int i = 0; i < arr.Length; i++)
        {
            int count = 1;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] == arr[j])
                    count++;
            }

            if (count >= 4)
                Console.Write(arr[i] + " ");
        }
    }
}