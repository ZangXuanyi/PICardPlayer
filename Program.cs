// See https://aka.ms/new-console-template for more information
using System.Net.Mail;

namespace PICardPlayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to card game!");
            EquipmentFactoryManager.RegisterFactories();
            GetCards(out Card card1, out Card card2);
            GetFightresults(card1, card2);
            return;
        }

        private static void GetFightresults(Card card1, Card card2)
        {
            if (card1.offensivePoint > card2.offensivePoint)
            {
                Console.WriteLine($"{card1.name} was faster!");
                int turn = 1;
                Console.WriteLine();
                while (card1.healthPoint > 0 && card2.healthPoint > 0)
                {
                    Console.WriteLine($"[Turn {turn}]");
                    card2.Attacked(card1);
                    if (card1.healthPoint <= 0 || card2.healthPoint <= 0) break;
                    card1.Attacked(card2);
                    turn++;
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"{card2.name} was faster!");
                int turn = 1;
                Console.WriteLine();
                while (card1.healthPoint > 0 && card2.healthPoint > 0)
                {
                    Console.WriteLine($"[Turn {turn}]");
                    card1.Attacked(card2);
                    if (card1.healthPoint <= 0 || card2.healthPoint <= 0) break;
                    card2.Attacked(card1);
                    turn++;
                    Console.WriteLine();
                }
            }
        }
        private static void GetCards(out Card card1, out Card card2)
        {
            Console.WriteLine("Please input the first card's properties, split by space:");
            Console.WriteLine("string name, int healthPoint, float firstHand, int type, string skillCodes(split by comma)");
            card1 = GetCardProperties();
            Console.WriteLine("Please input the second card's properties, split by space:");
            Console.WriteLine("string name, int healthPoint, float firstHand, int type, string skillCodes(split by comma)");
            card2 = GetCardProperties();
            Console.WriteLine();
            card1.PrintProperties();
            card2.PrintProperties();
        }
        private static Card GetCardProperties()
        {
        Loop:
            string? inline = Console.ReadLine();
            if (inline == null)
            {
                Console.WriteLine("Are you kidding me? Please input again!");
                goto Loop;
            }
            else
            {
                inline = inline.Trim();
                string[] strings = inline.Split([' ']);
                if (strings.Length != 4)
                {
                    Console.WriteLine("Are you kidding me? Please input again!");
                    goto Loop;
                }
                else
                {
                    Card card1 = new(
                        name: strings[0],
                        healthPoint: int.Parse(strings[1]),
                        firstHand: double.Parse(strings[2]),
                        type: int.Parse(strings[3]),
                        skills: strings[4]);
                    return card1;
                }
            }
        }
    }
}
