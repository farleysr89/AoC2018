using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
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
            var steps = new List<Step>();
            foreach (var s in data.Where(s => s != ""))
            {
                var parts = s.Split(" ");
                var pre = parts[1][0];
                var end = parts[7][0];
                if (steps.All(ss => ss.Id != end))
                {
                    var step = new Step { Id = end, Prerequisites = new List<char>(pre) };
                    step.Prerequisites.Add(pre);
                    steps.Add(step);
                }
                else
                {
                    steps.First(ss => ss.Id == end).Prerequisites.Add(pre);
                }
                if (steps.All(ss => ss.Id != pre))
                {
                    steps.Add(new Step { Id = pre, Prerequisites = new List<char>() });
                }
            }

            var solution = "";
            while (steps.Any())
            {
                var next = steps.Where(s => s.Prerequisites.Count == 0).OrderBy(s => s.Id).First();
                solution += next.Id;
                steps.Remove(next);
                foreach (var step in steps.Where(s => s.Prerequisites.Contains(next.Id)))
                {
                    step.Prerequisites.Remove(next.Id);
                }
            }
            Console.WriteLine("Solution = " + solution);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }
    }

    internal class Step
    {
        internal char Id;
        internal List<char> Prerequisites;
    }
}
