using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_03
{
    public class Entry
    {
        public int top, left, right;

        public Entry(int top, int left, int right)
        {
            this.top = top;
            this.left = left;
            this.right = right;
        }
    }

    public class Number : Entry
    {
        public int value;

        public Number(int top, int left, int right, int value) : base(top, left, right)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"({top},{left},{right - left}): {value}";
        }
    }

    public class Symbol : Entry
    {
        public string value;

        public Symbol(int top, int left, int right, string value) : base(top, left, right)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"({top},{left},{right - left}): {value}";
        }
    }

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

        static List<Entry> ReadInput(bool test = false)
        {
            List<Entry> result = new List<Entry>();

            string[] lines = GetLines(test);

            for (int top = 0; top < lines.Length; top++)
            {
                string line = lines[top];

                for (int left = 0; left < line.Length; left++)
                {
                    string cell = line[left].ToString();

                    if ("0123456789".Contains(cell))
                    {
                        // it's a digit, advance the for loop counter (left) until we consume the number
                        string numString = cell;
                        int startLeft = left;

                        left++;
                        while (left < line.Length && "0123456789".Contains(line[left].ToString()))
                        {
                            numString += line[left].ToString();
                            left++;
                        }
                        left--;

                        result.Add(new Number(top, startLeft, left, int.Parse(numString)));
                    } else if (cell == ".")
                    {
                        // it's blank, ignore
                    } else
                    {
                        // it's a symbol
                        result.Add(new Symbol(top, left, left, cell));
                    }
                }
            }

            return result;
        }

        static void Part1()
        {
            List<Entry> input = ReadInput(false);

            foreach(Entry entry in input) {
                Console.WriteLine(entry);
            }
            Console.WriteLine();

            List<Number> numbers = input.Where(e => e is Number).Select(e => (Number) e).ToList();
            List<Symbol> symbols = input.Where(e => e is Symbol).Select(e => (Symbol) e).ToList();

            int total = 0;
            foreach (Number n in numbers)
            {
                // Check if there is a symbol nearby
                bool symbolNearby = false;

                foreach (Symbol symbol in symbols)
                {
                    if (symbol.top >= n.top - 1 &&
                        symbol.top <= n.top + 1 &&
                        symbol.left >= n.left - 1 &&
                        symbol.right <= n.right + 1)
                    {
                        symbolNearby = true;

                        Console.WriteLine($"Symbol [{symbol}] is nearby number [{n}]");
                    }
                }

                if (symbolNearby) total += n.value;
            }

            Console.WriteLine(total);
        }

        static void Part2()
        {
            List<Entry> input = ReadInput(false);
            List<Number> numbers = input.Where(e => e is Number).Select(e => (Number)e).ToList();
            List<Symbol> symbols = input.Where(e => e is Symbol).Select(e => (Symbol)e).ToList();

            int total = 0;
            foreach (Symbol symbol in symbols)
            {
                if (symbol.value == "*")
                {
                    List<Number> adjacentNums = new List<Number>();
                    foreach (Number n in numbers)
                    {
                        if (symbol.top >= n.top - 1 &&
                        symbol.top <= n.top + 1 &&
                        symbol.left >= n.left - 1 &&
                        symbol.right <= n.right + 1)
                        {
                            adjacentNums.Add(n);
                            Console.WriteLine($"Symbol [{symbol}] is nearby number [{n}]");
                        }
                    }
                    if (adjacentNums.Count == 2)
                    {
                        Console.WriteLine("That symbol was a gear! Gear ratio: " + (adjacentNums[0].value * adjacentNums[1].value));
                        total += adjacentNums[0].value * adjacentNums[1].value;
                    } else
                    {
                        Console.WriteLine("That symbol was not a gear!");
                    }
                }
            }
            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            Part2();

            Console.ReadKey();
        }
    }
}
