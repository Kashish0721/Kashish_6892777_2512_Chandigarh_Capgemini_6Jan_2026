using System.Text.RegularExpressions;

class Logic
{
    public static int ValidateHex(string color)
    {
        return Regex.IsMatch(color, "^#[0-9A-Fa-f]{6}$") ? 1 : -1;
    }
}
