using System;
using System.Text;

class UserProgramCode
{
    public static string negativeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        StringBuilder result = new StringBuilder();
        int i = 0;

        while (i < input.Length)
        {
            // Check for word "is"
            if (i + 1 < input.Length &&
                input[i] == 'i' && input[i + 1] == 's' &&
                (i == 0 || !char.IsLetter(input[i - 1])) &&
                (i + 2 == input.Length || !char.IsLetter(input[i + 2])))
            {
                result.Append("is not");
                i += 2;
            }
            else
            {
                result.Append(input[i]);
                i++;
            }
        }

        return result.ToString();
    }
}
