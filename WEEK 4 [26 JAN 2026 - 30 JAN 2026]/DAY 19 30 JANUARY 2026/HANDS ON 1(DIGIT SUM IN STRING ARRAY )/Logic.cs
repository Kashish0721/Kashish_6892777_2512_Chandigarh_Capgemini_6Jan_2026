using System;

class UserProgramCode
{
    public static int sumOfDigits(string[] input1)
    {
        int sum = 0;

        foreach (string s in input1)
        {
            foreach (char c in s)
            {
                // Business Rule: special characters not allowed
                if (!char.IsLetterOrDigit(c))
                    return -1;

                // Add each digit separately
                if (char.IsDigit(c))
                    sum += (c - '0');
            }
        }

        return sum;
    }
}
