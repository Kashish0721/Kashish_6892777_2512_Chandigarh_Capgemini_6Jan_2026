using System;
using System.Text;
using System.Xml.Serialization;

class Program()
{
    static string CompressedString(string inp)
    {
        if (string.IsNullOrEmpty(inp)) return "";


        StringBuilder res = new StringBuilder();

        char current = inp[0];
        int count = 1;

        for (int i = 0; i < inp.Length; i++)
        {
            if (inp[i] == current)
            {
                count++;
            }
            else
            {
                res.Append(current);
                res.Append(count);

                current = inp[i];
                count = 1;
            }


        }
        res.Append(current);
        res.Append(count);
        return res.ToString();
    }
    static void Main()
    {
        string input = "aabbbbeeeeffggg";
        string output = CompressedString(input);

        Console.WriteLine(output);
    }
}

