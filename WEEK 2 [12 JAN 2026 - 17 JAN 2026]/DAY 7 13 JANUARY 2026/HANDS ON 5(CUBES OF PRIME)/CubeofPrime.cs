using System;

class CubeofPrimes
{
    static void Main()
    {
        int input1 = 10;
        int output = 0;

        
        if (input1 < 0)
        {
            output = -1;
        }
        else if (input1 > 32767)
        {
            output = -2;
        }
        else
        {
            int sum = 0;

            for (int i = 2; i <= input1; i++)
            {
                int flag = 0;

            
                for (int j = 2; j <= i / 2; j++)
                {
                    if (i % j == 0)
                    {
                        flag = 1;
                        break;
                    }
                }

                
                if (flag == 0)
                {
                    sum = sum + (i * i * i);
                }
            }

            output = sum;
        }

        Console.WriteLine("Output: " + output);
    }
}
