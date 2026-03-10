using System;

class SearchEle
{
    static void Main()
    {
        int[] arr = { 4, 6, 8, 2, 9 };
        int size = 5;  
        int ele = 2;   

        int output = 0;

    
        if (size < 0)
        {
            output = -2;
        }
        else
        {
            int flag = 0;
            for (int i = 0; i < size; i++)
            {
                if (arr[i] < 0)
                {
                    output = -1;
                    flag = 1;
                    break;
                }
            }

            if (flag == 0)
            {
                int found = 0;
                for (int i = 0; i < size; i++)
                {
                    if (arr[i] == ele)
                    {
                        output = 1;  
                        found = 1;
                        break;
                    }
                }

                if (found == 0)
                {
                    output = -3; 
                }
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
