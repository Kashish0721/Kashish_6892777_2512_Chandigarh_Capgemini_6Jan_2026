using System;
class RemoveRepeatedEle
{
    static void Main()
    {
        int[] arr = { 1, 2, 2, 3, 3 };
        int size = arr.Length;

        int[] outArr = new int[size];
        int k = 0;

        for (int i = 0; i < size; i++)
        {
            if (arr[i] < 0)
            {
                outArr[0] = -1;
                Console.WriteLine(outArr[0]);
                return;
            }

            int flag = 0;   // 0 = not found, 1 = found

            for (int j = 0; j < k; j++)
            {
                if (outArr[j] == arr[i])
                {
                    flag = 1;
                    break;
                }
            }

            if (flag == 0)
            {
                outArr[k] = arr[i];
                k++;
            }
        }

        for (int i = 0; i < k; i++)
            Console.Write(outArr[i] + " ");

        Console.ReadLine();
    }
}
