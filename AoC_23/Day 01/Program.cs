using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_01
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

        static void Main(string[] args)
        {
            string[] lines = GetLines(true);
            Console.WriteLine(ArrayToString(lines));



            Console.ReadKey();
        }
    }
}
