using System;

class Program
{
    static void Main()
    {
        int[] nums = { 1, 2, 3, 4, 5, 6, 7 };
        int k = 3;

        Rotate(nums, k);

        foreach (int x in nums)
            Console.Write(x + " ");
    }

    static void Rotate(int[] nums, int k)
    {
        int n = nums.Length;
        k = k % n;

        Reverse(nums, 0, n - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, n - 1);
    }

    static void Reverse(int[] a, int l, int r)
    {
        while (l < r)
        {
            int t = a[l];
            a[l] = a[r];
            a[r] = t;
            l++; r--;
        }
    }
}
