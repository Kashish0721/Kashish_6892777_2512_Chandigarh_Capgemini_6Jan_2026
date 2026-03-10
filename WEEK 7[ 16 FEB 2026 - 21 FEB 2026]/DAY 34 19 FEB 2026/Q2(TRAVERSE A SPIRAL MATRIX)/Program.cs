using System;
using System.Collections.Generic;

namespace SpiralMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] matrix =
            {
                new int[]{1,2,3,4},
                new int[]{5,6,7,8},
                new int[]{9,10,11,12},
                new int[]{13,14,15,16},
                new int[]{17,18,19,20}
            };

            Solution obj = new Solution();
            IList<int> result = obj.SpiralOrder(matrix);

            foreach (var x in result)
                Console.Write(x + " ");
        }
    }

    public class Solution
    {
        public IList<int> SpiralOrder(int[][] matrix)
        {
            int n = matrix.Length;
            int m = matrix[0].Length;

            int left = 0, right = m - 1;
            int top = 0, bottom = n - 1;

            List<int> answer = new List<int>();

            while (top <= bottom && left <= right)
            {
                // top row
                for (int i = left; i <= right; i++)
                    answer.Add(matrix[top][i]);
                top++;

                // right column
                for (int i = top; i <= bottom; i++)
                    answer.Add(matrix[i][right]);
                right--;

                // bottom row
                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--)
                        answer.Add(matrix[bottom][i]);
                    bottom--;
                }

                // left column
                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--)
                        answer.Add(matrix[i][left]);
                    left++;
                }
            }
            return answer;
        }
    }
}
