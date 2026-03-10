using System;

class RemoveNegatives
{
    static void Main()
    {
        int[] input1 = { 3, -1, 5, -4, 2, 0 };
        int size = 6; 

   
        if (size < 0)
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
           
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                if (input1[i] >= 0)
                    count++;
            }

            int[] result = new int[count];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                if (input1[i] >= 0)
                {
                    result[index] = input1[i];
                    index++;
                }
            }

            for (int i = 0; i < result.Length - 1; i++)
            {
                for (int j = i + 1; j < result.Length; j++)
                {
                    if (result[i] > result[j])
                    {
                        int temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }

            Console.WriteLine("Output Array:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
        }
    }
}
