using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_02
{
    public class Handful
    {
        public int red, green, blue;

        public Handful(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public override string ToString()
        {
            return $"{red}R{green}G{blue}B";
        }

        public int Power()
        {
            return red * green * blue;
        }
    }

    public class Game
    {
        public int id;
        public List<Handful> handfuls = new List<Handful>();

        public Game(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            string result = id + ": ";

            foreach (Handful handful in handfuls)
            {
                result += handful.ToString() + " ";
            }

            return result;
        }

        public bool isGamePossible(Handful bag)
        {
            Handful total = new Handful(0, 0, 0);

            foreach(Handful handful in handfuls)
            {
                total.red += handful.red;
                total.green += handful.green;
                total.blue += handful.blue;

                if (handful.red > bag.red) return false;
                if (handful.green > bag.green) return false;
                if (handful.blue > bag.blue) return false;
            }

            return true;
        }

        public Handful smallestBag()
        {
            Handful result = new Handful(0, 0, 0);

            foreach (Handful handful in handfuls)
            {
                if (handful.red > result.red) result.red = handful.red;
                if (handful.green > result.green) result.green = handful.green;
                if (handful.blue > result.blue) result.blue = handful.blue;
            }

            return result;
        }
    }

    internal class Program
    {
        static string[] GetLines(bool test = false)
        {
            return File.ReadAllLines(test ? "text.txt" : "input.txt");
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

        static List<Game> LoadGames(bool test = false)
        {
            string[] lines = GetLines(test);

            List<Game> games = new List<Game>();

            // Load file
            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { ": " }, StringSplitOptions.None);
                Game game = new Game(int.Parse(parts[0].Split(' ')[1]));

                foreach (string handfulString in parts[1].Split(new string[] { "; " }, StringSplitOptions.None))
                {
                    int r = 0, g = 0, b = 0;
                    string[] colourStrings = handfulString.Split(new string[] { ", " }, StringSplitOptions.None);

                    foreach (string colourString in colourStrings)
                    {
                        string[] colourParts = colourString.Split(' ');
                        switch (colourParts[1])
                        {
                            case "red": r = int.Parse(colourParts[0]); break;
                            case "green": g = int.Parse(colourParts[0]); break;
                            case "blue": b = int.Parse(colourParts[0]); break;
                        }
                    }
                    game.handfuls.Add(new Handful(r, g, b));
                }

                games.Add(game);
            }
            return games;
        }

        static void Part1()
        {
            List<Game> games = LoadGames(true);

            int total = 0;
            foreach (Game game in games)
            {
                if (game.isGamePossible(new Handful(12, 13, 14)))
                {
                    total += game.id;
                }
            }
            Console.WriteLine(total);
        }

        static void Part2()
        {
            List<Game> games = LoadGames(false);

            int total = 0;
            foreach (Game game in games)
            {
                Handful bag = game.smallestBag();
                total += bag.Power();
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
