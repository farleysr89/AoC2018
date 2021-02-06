using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    internal class Program
    {
        private static void Main()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var parts = data[0].Split(": ");
            var pots = parts[1].Aggregate("...", (current, c) => current + c);

            pots += "...";

            var rules = new List<Rule>();
            foreach (var line in data.Skip(2).Where(s => s != ""))
            {
                parts = line.Split(" => ");
                rules.Add(new Rule { Input = parts[0], Output = parts[1][0] });
            }
            long change = 0;
            long newCount = 0;
            long diff = 0;
            var index = -3;
            long lastX = 0;
            for (long x = 0; x < 20; x++)
            {
                lastX = x;
                var newS = "..";
                for (var i = 2; i < pots.Length - 2; i++)
                {
                    newS += rules.Any(r => pots[(i - 2)..(i + 3)] == r.Input) ? rules.First(r => pots[(i - 2)..(i + 3)] == r.Input).Output : '.';
                }

                pots = new string(newS);
                while (pots[0..3] != "...")
                {
                    index--;
                    pots = "." + pots;
                }

                while (pots.IndexOf('#') > 3)
                {
                    index++;
                    pots = pots[1..];
                }

                while (pots[^3..] != "...")
                {
                    pots += ".";
                }

                while (pots.LastIndexOf('#') < pots.Length - 4)
                {
                    pots = pots[..^1];
                }

                var newChange = PotSum(pots, index);
                if (newChange - change == diff)
                {
                    break;
                }
                diff = newChange - change;
                change = newChange;

            }

            newCount = PotSum(pots, index);
            if (lastX < 19)
            {
                newCount += (diff * (19 - lastX));
            }
            Console.WriteLine("Total count = " + newCount);
        }

        internal static long PotSum(string pots, int index)
        {
            var newCount = 0;
            foreach (var c in pots)
            {
                if (c == '#') newCount += index;
                index++;
            }

            return newCount;
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var parts = data[0].Split(": ");
            var pots = parts[1].Aggregate("...", (current, c) => current + c);

            pots += "...";

            var rules = new List<Rule>();
            foreach (var line in data.Skip(2).Where(s => s != ""))
            {
                parts = line.Split(" => ");
                rules.Add(new Rule { Input = parts[0], Output = parts[1][0] });
            }
            long change = 0;
            long newCount = 0;
            long diff = 0;
            var index = -3;
            long lastX = 0;
            for (long x = 0; x < 50000000000; x++)
            {
                lastX = x;
                var newS = "..";
                for (var i = 2; i < pots.Length - 2; i++)
                {
                    newS += rules.Any(r => pots[(i - 2)..(i + 3)] == r.Input) ? rules.First(r => pots[(i - 2)..(i + 3)] == r.Input).Output : '.';
                }

                pots = new string(newS);
                while (pots[0..3] != "...")
                {
                    index--;
                    pots = "." + pots;
                }

                while (pots.IndexOf('#') > 3)
                {
                    index++;
                    pots = pots[1..];
                }

                while (pots[^3..] != "...")
                {
                    pots += ".";
                }

                while (pots.LastIndexOf('#') < pots.Length - 4)
                {
                    pots = pots[..^1];
                }

                var newChange = PotSum(pots, index);
                if (newChange - change == diff)
                {
                    break;
                }
                diff = newChange - change;
                change = newChange;

            }

            newCount = PotSum(pots, index);
            if (lastX < 49999999999)
            {
                newCount += (diff * (49999999999 - lastX));
            }
            Console.WriteLine("Total count = " + newCount);
        }
    }

    internal class Rule
    {
        internal string Input;
        internal char Output;
    }
}
