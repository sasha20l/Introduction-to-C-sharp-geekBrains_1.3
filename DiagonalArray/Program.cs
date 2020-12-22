using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagonalArray
{
    class Program
    {
        static void Main(string[] args)
        {
         int[,] nums = new int[10, 10];
         int rows = nums.GetLength(0);
         int columns = nums.GetLength(1);
         Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    nums[i,j] = rnd.Next(0, 9);
                    string view = (i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0) ? nums[i, j].ToString() : null;
                    Console.Write($"{view} \t");

                }
                Console.WriteLine();
              
            }
            Console.ReadLine();
        }
    }
}
