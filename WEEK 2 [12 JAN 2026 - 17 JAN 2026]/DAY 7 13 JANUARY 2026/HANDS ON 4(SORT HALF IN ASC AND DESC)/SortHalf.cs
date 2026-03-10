using System;

class SortHalf
{
    static void Main()
    {
        int[] arr = { 8, 3, 1, 9, 5, 2 };
        int size = 6;  

     
        if (size< 0)
        {
            int[] result = { -1 };
            Console.WriteLine("Output Array:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
        }
        else
        {
            int mid = size / 2;

            for (int i = 0; i < mid - 1; i++)
            {
                for (int j = i + 1; j < mid; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            for (int i = mid; i < size - 1; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            Console.WriteLine("Output Array:");
            for (int i = 0; i < size; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}

