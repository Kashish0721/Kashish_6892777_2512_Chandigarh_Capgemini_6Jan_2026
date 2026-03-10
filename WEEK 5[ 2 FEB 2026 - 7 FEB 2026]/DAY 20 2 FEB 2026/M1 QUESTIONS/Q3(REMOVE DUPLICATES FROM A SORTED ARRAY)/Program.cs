using System;

class Program
{
    static void Main()
    {
        int[] nums = { 0, 0, 1, 1, 2, 3, 3 };

        int k = RemoveDuplicates(nums);

        for (int i = 0; i < k; i++)
            Console.Write(nums[i] + " ");
    }

    static int RemoveDuplicates(int[] nums)
    {
        int k = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[i - 1])
            {
                nums[k] = nums[i];
                k++;
            }
        }
        return k;
    }
}
