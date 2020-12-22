using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] phonebook = new string[5, 2] { {"Александр","89825500067/sasha765ll@mail.ru" },{"Алексей", "89825500099/sasha34l@mail.ru" },{ "Дмитрий", "89825511167/sasha44@mail.ru" },{ "Анастасия", "89825599999/sasha4@mail.ru" },{ "Николай", "89824300067/sasha00@mail.ru" } };
            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                for (int j = 0; j < phonebook.GetLength(1); j++)
                {
                   
                    Console.Write($"{phonebook[i, j]} \t");

                }
                Console.WriteLine();

            }
            Console.ReadLine();
        }
    }
}
