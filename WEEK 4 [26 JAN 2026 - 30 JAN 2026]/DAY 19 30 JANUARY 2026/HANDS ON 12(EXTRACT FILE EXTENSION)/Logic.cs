using System;

class Logic
{
    public static string GetExtension(string file)
    {
        int idx = file.LastIndexOf('.');
        if (idx == -1 || idx == file.Length - 1)
            return "-1";

        return file.Substring(idx + 1);
    }
}
