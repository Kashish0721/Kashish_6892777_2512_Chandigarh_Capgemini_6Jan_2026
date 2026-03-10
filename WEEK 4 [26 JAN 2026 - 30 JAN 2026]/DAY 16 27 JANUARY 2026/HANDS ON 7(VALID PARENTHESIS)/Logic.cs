using System;
using System.Collections.Generic;

class Logic
{
    public string IsValidParentheses(string s)
    {
        Stack<char> st = new Stack<char>();

        foreach (char c in s)
        {
            if (c == '(' || c == '{' || c == '[')
                st.Push(c);
            else
            {
                if (st.Count == 0) return "NO";

                char top = st.Pop();
                if ((c == ')' && top != '(') ||
                    (c == '}' && top != '{') ||
                    (c == ']' && top != '['))
                    return "NO";
            }
        }

        return st.Count == 0 ? "YES" : "NO";
    }
}
