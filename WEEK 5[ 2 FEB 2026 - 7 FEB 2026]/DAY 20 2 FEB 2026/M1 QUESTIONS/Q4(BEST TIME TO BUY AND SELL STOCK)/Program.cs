using System;

class Program
{
    static void Main()
    {
        int[] prices = { 7, 1, 5, 3, 6, 4 };
        Console.WriteLine(MaxProfit(prices));
    }

    static int MaxProfit(int[] prices)
    {
        int min = prices[0];
        int profit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] < min)
                min = prices[i];
            else
                profit = Math.Max(profit, prices[i] - min);
        }
        return profit;
    }
}
