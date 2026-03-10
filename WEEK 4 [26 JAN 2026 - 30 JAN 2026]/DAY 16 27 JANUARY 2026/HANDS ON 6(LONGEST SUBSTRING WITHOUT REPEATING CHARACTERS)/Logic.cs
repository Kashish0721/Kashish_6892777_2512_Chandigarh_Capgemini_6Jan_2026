using System;
using System.Collections.Generic;

class Logic
{
    public int LengthOfLongestSubstring(string s)
    {
        Dictionary<char, int> lastIndex = new Dictionary<char, int>();
        int maxLen = 0;
        int start = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (lastIndex.ContainsKey(s[i]) && lastIndex[s[i]] >= start)
            {
                start = lastIndex[s[i]] + 1;
            }

            lastIndex[s[i]] = i;
            maxLen = Math.Max(maxLen, i - start + 1);
        }

        return maxLen;
    }
}
