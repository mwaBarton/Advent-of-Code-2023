using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_04
{
    public class Card
    {
        public int id;
        public List<int> winningNumbers = new List<int>();
        public List<int> numbersYouHave = new List<int>();

        public Card(int id)
        {
            this.id = id;
        }

        public List<int> GetWinningNumbersThatYouHave()
        {
            return winningNumbers.Where(n => numbersYouHave.Contains(n)).ToList();
        }

        public override string ToString()
        {
            string result = $"Card {id}: ";

            result += winningNumbers.Select(n => n.ToString()).Aggregate((n1, n2) => n1 + " " + n2) + " | ";
            result += numbersYouHave.Select(n => n.ToString()).Aggregate((n1, n2) => n1 + " " + n2);

            return result;
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

        static List<Card> GetCards(bool test = false)
        {
            List<Card> cards = new List<Card>();

            string[] lines = GetLines(test);

            foreach (string line in lines)
            {
                string[] sides = line.Split(new string[] { ": " }, StringSplitOptions.None);
                Card card = new Card(int.Parse(sides[0].Split(' ').Where(s => s.Length > 0).ToArray()[1]));

                sides = sides[1].Split(new string[] { " | " }, StringSplitOptions.None);

                card.winningNumbers = sides[0].Split(' ').Where(s => s.Length > 0).Select(int.Parse).ToList();
                card.numbersYouHave = sides[1].Split(' ').Where(s => s.Length > 0).Select(int.Parse).ToList();

                cards.Add(card);
            }

            return cards;
        }

        static void Part1()
        {
            List<Card> cards = GetCards(false);

            int total = 0;
            foreach (Card card in cards)
            {
                var nums = card.GetWinningNumbersThatYouHave();
                Console.WriteLine($"{card.id}: {ArrayToString(nums.ToArray())}");

                if (nums.Count > 0)
                {
                    total += (int)Math.Pow(2, nums.Count - 1);
                }
            }
            Console.WriteLine(total);
        }

        static void Part2()
        {
            Card[] originalCards = GetCards(false).ToArray();

            List<Card> cardsWon = originalCards.ToList();

            int count = 0;
            while (count < cardsWon.Count)
            {
                var card = cardsWon[count];
                var nums = card.GetWinningNumbersThatYouHave();
                if (nums.Count > 0)
                {
                    for (int i = card.id + 1; i <= card.id + nums.Count; i++)
                    {
                        cardsWon.Add(originalCards[i - 1]);
                    }
                }
                count++;
            }

            Console.WriteLine(cardsWon.Count);
        }

        static void Main(string[] args)
        {
            Part2();

            Console.ReadKey();
        }
    }
}
