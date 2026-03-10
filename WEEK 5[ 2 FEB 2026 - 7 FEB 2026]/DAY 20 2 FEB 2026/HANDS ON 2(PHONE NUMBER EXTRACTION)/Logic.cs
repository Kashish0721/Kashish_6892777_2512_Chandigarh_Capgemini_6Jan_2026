using System;

class Logic
{
    public static void ExtractPhoneNumbers(string text)
    {
        for (int i = 0; i <= text.Length - 10; i++)
        {
            string sub = text.Substring(i, 10);
            bool allDigits = true;

            foreach (char c in sub)
            {
                if (!char.IsDigit(c))
                {
                    allDigits = false;
                    break;
                }
            }

            if (allDigits)
                Console.WriteLine(sub);
        }
    }
}
