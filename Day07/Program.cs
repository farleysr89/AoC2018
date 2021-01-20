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
            var steps = new List<Step>();
            foreach (var s in data.Where(s => s != ""))
            {
                var parts = s.Split(" ");
                var pre = parts[1][0];
                var end = parts[7][0];
                if (steps.All(ss => ss.Id != end))
                {
                    var step = new Step { Id = end, Prerequisites = new List<char>(pre), Time = end - 4 };
                    step.Prerequisites.Add(pre);
                    steps.Add(step);
                }
                else
                {
                    steps.First(ss => ss.Id == end).Prerequisites.Add(pre);
                }
                if (steps.All(ss => ss.Id != pre))
                {
                    steps.Add(new Step { Id = pre, Prerequisites = new List<char>(), Time = pre - 4 });
                }
            }

            var workers = new List<Worker>
            {
                new Worker(),
                new Worker(),
                new Worker(),
                new Worker(),
                new Worker()
            };
            var time = 0;
            while (steps.Any())
            {
                foreach (var worker in workers.Where(w => w.InProgress != null))
                {
                    worker.TimeLeft--;
                }
                foreach (var worker in workers.Where(w => w.InProgress != null && w.TimeLeft == 0))
                {
                    foreach (var step in steps.Where(s => s.Prerequisites.Contains(worker.InProgress.Id)))
                    {
                        step.Prerequisites.Remove(worker.InProgress.Id);
                    }

                    worker.InProgress = null;
                }
                while (workers.Any(w => w.InProgress == null) && steps.Any(s => s.Prerequisites.Count == 0))
                {
                    var work = workers.First(w => w.InProgress == null);
                    var next = steps.Where(s => s.Prerequisites.Count == 0).OrderBy(s => s.Id).First();
                    steps.Remove(next);
                    work.InProgress = next;
                    work.TimeLeft = next.Time;
                }

                time++;
            }

            time += workers.Max(w => w.TimeLeft);
            Console.WriteLine("Time = " + (time - 1));
        }
    }

    internal class Step
    {
        internal char Id;
        internal List<char> Prerequisites;
        internal int Time;
    }

    internal class Worker
    {
        internal Step InProgress;
        internal int TimeLeft;
    }
}
