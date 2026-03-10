using System;

class Logic
{
    public int CalculateGrossSalary(int basic, int days)
    {
     
        if (basic < 0)
            return -1;

        if (basic > 10000)
            return -2;

        if (days > 31)
            return -3;

        int da = (int)(basic * 0.75);
        int hra = (int)(basic * 0.50);

        int grossSalary = basic + da + hra;

        return grossSalary;
    }
}
