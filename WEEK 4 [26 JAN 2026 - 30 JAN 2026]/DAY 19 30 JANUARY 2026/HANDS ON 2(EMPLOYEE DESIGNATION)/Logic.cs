using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static string[] getEmployee(string[] input1, string input2)
    {
        // Business Rule 1: special characters not allowed
        foreach (string s in input1)
            foreach (char c in s)
                if (!char.IsLetter(c))
                    return new string[] { "Invalid Input" };

        foreach (char c in input2)
            if (!char.IsLetter(c) && c != ' ')
                return new string[] { "Invalid Input" };

        List<string> employees = new List<string>();
        bool anyOtherDesignation = false;

        for (int i = 0; i < input1.Length - 1; i += 2)
        {
            string emp = input1[i];
            string desig = input1[i + 1];

            if (string.Equals(desig, input2, StringComparison.OrdinalIgnoreCase))
                employees.Add(emp);
            else
                anyOtherDesignation = true;
        }

        if (employees.Count == 0)
            return new string[] { "No employee for " + input2 + " designation" };

        if (!anyOtherDesignation)
            return new string[] { "All employees belong to same " + input2 + " designation" };

        return employees.ToArray();
    }
}
