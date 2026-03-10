using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string invoiceNumber = "CAP-HYD-16541";
        string newLocation = "GRGN";

        Regex regex = new Regex(@"CAP-([A-Z]+)-(\d+)");
        Match match = regex.Match(invoiceNumber);


        if(match.Success ) {
            string updatedInvoice = "CAP-" + newLocation + "-" + match.Groups[2].Value;
            Console.WriteLine(updatedInvoice);
        }

    }
}