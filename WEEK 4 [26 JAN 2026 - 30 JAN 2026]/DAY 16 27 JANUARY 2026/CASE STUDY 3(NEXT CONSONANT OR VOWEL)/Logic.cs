using System;

class UserProgramCode
{
    public static string nextString(string input1)
    {
        // Business Rule: no numbers or special characters
        foreach (char c in input1)
        {
            if (!char.IsLetter(c))
                return "Invalid input";
        }

        string vowels = "aeiou";
        string result = "";

        foreach (char c in input1)
        {
            bool isUpper = char.IsUpper(c);
            char ch = char.ToLower(c);

            if (vowels.Contains(ch))
            {
                // Replace vowel with next consonant
                char next = (char)(ch + 1);
                while (vowels.Contains(next))
                    next++;

                result += isUpper ? char.ToUpper(next) : next;
            }
            else
            {
                // Replace consonant with next vowel
                char next = ch;
                while (!vowels.Contains(next))
                {
                    next++;
                    if (next > 'z') next = 'a';
                }

                result += isUpper ? char.ToUpper(next) : next;
            }
        }

        return result;
    }
}
