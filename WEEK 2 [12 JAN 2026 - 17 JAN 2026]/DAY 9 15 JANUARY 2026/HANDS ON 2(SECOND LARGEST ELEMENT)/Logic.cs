using System;

class Logic
{
    public int SecondLargest(int[] input1, int input3)
    {
        
        if (input3 < 0)
            return -2;

        foreach (int x in input1)
            if (x < 0)
                return -1;

        int largest = int.MinValue;
        int secondLargest = int.MinValue;

        for (int i = 0; i < input3; i++)
        {
            if (input1[i] > largest)
            {
                secondLargest = largest;
                largest = input1[i];
            }
            else if (input1[i] > secondLargest && input1[i] != largest)
            {
                secondLargest = input1[i];
            }
        }

        return secondLargest;
    }
}
