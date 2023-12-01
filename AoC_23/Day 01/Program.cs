using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

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

        static void Part1()
        {
            string[] lines = GetLines(false);
            //Console.WriteLine(ArrayToString(lines));

            List<int> nums = new List<int>();
            foreach (string line in lines)
            {
                string numString = "";
                int num;

                // First digit
                for (int i = 0; i < line.Length; i++)
                {
                    if (int.TryParse(line[i].ToString(), out num))
                    {
                        numString += num.ToString();
                        break;
                    }
                }

                // Last digit
                for (int i = line.Length - 1; i >= 0; i--)
                {
                    if (int.TryParse(line[i].ToString(), out num))
                    {
                        numString += num.ToString();
                        break;
                    }
                }

                nums.Add(int.Parse(numString));
                //Console.WriteLine(numString);
            }

            Console.WriteLine(nums.Sum());
        }

        static Dictionary<string, int> GetNumLastWords()
        {
            Dictionary<string, int> result = new Dictionary<string, int>
            {
                { "zero", 0 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
                { "ten", 0 },
                { "eleven", 1 },
                { "twelve", 2 },
                { "thirteen", 3 },
                { "fourteen", 4 },
                { "fifteen", 5 },
                { "sixteen", 6 },
            };

            return result;
        }

        static Dictionary<string, int> GetNumFirstWords()
        {
            Dictionary<string, int> result = new Dictionary<string, int>
            {
                { "zero", 0 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
                { "ten", 1 },
                { "eleven", 1 },
                { "twelve", 1 },
                { "thirteen", 1 },
                { "fourteen", 1 },
                { "fifteen", 1 },
                { "sixteen", 1 },
            };

            return result;
        }

        static int GetFirstDigit(string line)
        {
            string numString, currentTest = "";
            int num;
            var numWords = GetNumFirstWords();

            // First digit
            for (int i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i].ToString(), out num))
                {
                    return num;
                }
                else
                {
                    // It's a letter
                    int j = i;
                    currentTest = line[j].ToString();

                    var possibleWords = numWords.Keys.Where(w => w.IndexOf(currentTest) == 0).ToList();
                    
                    while (possibleWords.Count > 0)
                    {
                        foreach (var possibleWord in possibleWords)
                        {
                            if (possibleWord == currentTest)
                            {
                                // Found it
                                return numWords[possibleWord];
                            }
                        }
                        j++;
                        currentTest += line[j].ToString();
                        possibleWords = numWords.Keys.Where(w => w.IndexOf(currentTest) == 0).ToList();
                    }
                }
            }

            return 0;
        }

        static int GetSecondDigit(string line)
        {
            string numString, currentTest = "";
            int num;
            var numWords = GetNumLastWords();

            // second digit
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (int.TryParse(line[i].ToString(), out num))
                {
                    return num;
                }
                else
                {
                    // It's a letter
                    int j = i;
                    currentTest = line[j].ToString();

                    var possibleWords = numWords.Keys.Where(w => w.IndexOf(currentTest) + currentTest.Length == w.Length).ToList();

                    while (possibleWords.Count > 0)
                    {
                        foreach (var possibleWord in possibleWords)
                        {
                            if (possibleWord == currentTest)
                            {
                                // Found it
                                return numWords[possibleWord];
                            }
                        }
                        j--;
                        currentTest = line[j].ToString() + currentTest;
                        possibleWords = numWords.Keys.Where(w => w.IndexOf(currentTest) + currentTest.Length == w.Length).ToList();
                    }
                }
            }

            return 0;
        }
        static void Part2()
        {
            string[] lines = GetLines(false);

            List<int> nums = new List<int>();
            foreach (string line in lines)
            {
                int firstDigit = GetFirstDigit(line);
                //Console.WriteLine(firstDigit);

                int secondDigit = GetSecondDigit(line);
                //Console.WriteLine(secondDigit);

                nums.Add(int.Parse(firstDigit.ToString() + secondDigit.ToString()));
                //Console.WriteLine(nums.Last());
            }

            Console.WriteLine(nums.Sum());
        }

        static void Main(string[] args)
        {
            Part2();

            Console.ReadKey();
        }
    }
}
