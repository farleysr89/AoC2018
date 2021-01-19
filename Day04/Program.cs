using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
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
            var data = input.Split('\n').Where(s => s != "").OrderBy(s => s).ToList();
            var guards = new List<Guard>();
            Guard g = null;
            var start = 0;
            foreach (var parts in data.Select(s => s.Split(" ")))
            {
                switch (parts[2])
                {
                    case "Guard":
                        var id = int.Parse(parts[3][1..]);
                        if (guards.Any(a => a.Id == id)) g = guards.First(a => a.Id == id);
                        else
                        {
                            g = new Guard { Id = int.Parse(parts[3][1..]) };
                            guards.Add(g);
                        }

                        break;
                    case "falls":
                        start = int.Parse(parts[1].Split(":")[1][..^1]);
                        break;
                    case "wakes":
                        var finish = int.Parse(parts[1].Split(":")[1][..^1]);
                        for (var i = start; i < finish; i++) g.SleepTimes[i]++;
                        break;
                    default:
                        Console.WriteLine("Something broke!");
                        break;
                }
            }

            guards = guards.OrderByDescending(g => g.SleepTimes.Sum()).ToList();
            var m = guards.First().SleepTimes.Max();
            var index = guards.First().SleepTimes.ToList().IndexOf(m);
            Console.WriteLine("Solution = " + (guards.First().Id * index));
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }
    }

    internal class Guard
    {
        internal int Id;
        internal int[] SleepTimes = new int[60];
    }
}
