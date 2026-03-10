using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter string: ");
        string s = Console.ReadLine();

        StringBuilder sb = new StringBuilder(s);

        bool found = true;

        while (found)
        {
            found = false;
            StringBuilder temp = new StringBuilder();

            for (int i = 0; i < sb.Length; i++)
            {
                if (i < sb.Length - 1 && sb[i] == sb[i + 1])
                {
                    found = true;
                    while (i < sb.Length - 1 && sb[i] == sb[i + 1])
                        i++;
                }
                else
                {
                    temp.Append(sb[i]);
                }
            }

            sb = temp;
        }

        Console.WriteLine("Final String: " + sb);
    }
}
