using System;

class Logic
{
    public static int IsEligibleToVote(int age)
    {
        if (age < 0)
            return -1;

        return age >= 18 ? 1 : 0;
    }
}
