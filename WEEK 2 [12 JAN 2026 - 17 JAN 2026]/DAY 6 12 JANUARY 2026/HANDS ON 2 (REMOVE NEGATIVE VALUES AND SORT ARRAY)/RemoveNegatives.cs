using System;
using System.Collections.Generic;
using System.Text;

namespace HANDS_ON_2__REMOVE_NEGATIVE_VALUES_AND_SORT_ARRAY_
{
    internal class RemoveNegatives
    {
        static void Main()
        {
            int [] array = { 1, 2, 3, -2, 4 };
            int size = array.Length;

            if(size < 0)
            {
                Console.WriteLine("-1");
                return;
            }
           
            int[] temp = new int[size];
            int k = 0;

            for (int i = 0; i < size; i++)
                if (array[i] >= 0)
                {
                    temp[k++] = array[i];
                }
            for (int i = 0; i < k; i++)
                for (int j = i + 1; j < k; j++)
                    if (temp[i] > temp[j])
                    {
                        int t = temp[i];
                        temp[i] = temp[j];
                        temp[j] = t;
                    }

            for (int i = 0; i < k; i++)
                Console.Write(temp[i] + " ");

            Console.ReadLine();
        }
    }
}

