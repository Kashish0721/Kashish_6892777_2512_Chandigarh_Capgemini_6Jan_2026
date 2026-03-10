using System;

class Logic
{
    public int[] ShipStory(int[] input1, int[] input2, int size)
    {
      
        if (size < 0)
            return new int[] { -2 };

        for (int i = 0; i < size; i++)
        {
           
            if (input1[i] < 0 || input2[i] < 0)
                return new int[] { -1 };
        }

        int[] output = new int[size];

        for (int i = 0; i < size; i++)
        {
            output[i] = input1[i] + input2[size - 1 - i];
        }

        return output;
    }
}
