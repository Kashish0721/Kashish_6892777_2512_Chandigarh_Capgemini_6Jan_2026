using System;

class UserProgramCode
{
    public static int GetCount(int size, string[] input1, char input2)
    {
        int count = 0;

        foreach (string s in input1)
        {
            // Business Rule 2: Only alphabets allowed
            foreach (char c in s)
            {
                if (!char.IsLetter(c))
                    return -2;
            }

            if (s.Length > 0 && char.ToLower(s[0]) == char.ToLower(input2))
                count++;
        }

        // Business Rule 1: No element found
        if (count == 0)
            return -1;

        return count;
    }
}
