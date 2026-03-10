using System;

class UserProgramCode
{
    public static string replaceString(string input1, int input2, char input3)
    {
        // Business Rule 1: input1 must contain only alphabets and spaces
        foreach (char c in input1)
        {
            if (!char.IsLetter(c) && c != ' ')
                return "-1";
        }

        // Business Rule 2: input2 must be positive
        if (input2 <= 0)
            return "-2";

        // Business Rule 3: input3 must be a special character
        if (char.IsLetterOrDigit(input3))
            return "-3";

        string[] words = input1.Split(' ');

        if (input2 > words.Length)
            return input1.ToLower(); // no replacement needed

        int index = input2 - 1;
        int len = words[index].Length;

        // Replace nth word with special characters
        words[index] = new string(input3, len);

        return string.Join(" ", words).ToLower();
    }
}
