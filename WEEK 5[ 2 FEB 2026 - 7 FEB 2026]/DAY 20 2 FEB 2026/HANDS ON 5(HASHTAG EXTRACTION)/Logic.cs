using System;

class Logic
{
    public static void ExtractHashtags(string text)
    {
        string[] words = text.Split(' ');

        foreach (string w in words)
        {
            if (w.StartsWith("#") && w.Length > 1)
                Console.WriteLine(w);
        }
    }
}
