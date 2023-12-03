using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_04
{
    internal class Program
    {
        static string[] GetLines(bool test = false)
        {
            return File.ReadAllLines(test ? "test.txt" : "input.txt");
        }

        static string ArrayToString<T>(T[] array, bool newLine = false)
        {
            string result = "[";
            if (newLine) result += "\n";

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString();

                if (i < array.Length - 1)
                {
                    result += ", ";
                    if (newLine) result += "\n";
                }
            }

            if (newLine) result += "\n";
            result += "]";

            return result;
        }

        static void Part1()
        {
            
        }

        static void Part2()
        {
            
        }

        static void Main(string[] args)
        {
            Part1();

            Console.ReadKey();
        }
    }
}
