using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string invoiceNumber = "CAP-123";
        int increment = 7;

        // Regex to capture numeric part
        Regex regex = new Regex(@"CAP-(\d+)");
        Match match = regex.Match(invoiceNumber);

        if (match.Success)
        {
            int number = int.Parse(match.Groups[1].Value);
            int updatedNumber = number + increment;

            string newInvoice = "CAP-" + updatedNumber;
            Console.WriteLine(newInvoice);
        }
    }
}
