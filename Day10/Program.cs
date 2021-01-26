using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
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
            var stars = (from s in data.Where(s => s != "") select s.Split('<', '>') into parts let first = parts[1].Split(",").Select(int.Parse).ToList() let second = parts[3].Split(",").Select(int.Parse).ToList() select new Star { X = first[0], Y = first[1], XVelocity = second[0], YVelocity = second[1] }).ToList();

            var minDiff = int.MaxValue;
            while (true)
            {
                stars.ForEach(s => s.Move(true));
                var tmp = Math.Abs(stars.Max(s => s.Y) - stars.Min(s => s.Y));
                if (tmp > minDiff) break;
                minDiff = tmp;
            }
            stars.ForEach(s => s.Move(false));
            Print(stars);

            Console.WriteLine("");
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var stars = (from s in data.Where(s => s != "") select s.Split('<', '>') into parts let first = parts[1].Split(",").Select(int.Parse).ToList() let second = parts[3].Split(",").Select(int.Parse).ToList() select new Star { X = first[0], Y = first[1], XVelocity = second[0], YVelocity = second[1] }).ToList();

            var minDiff = int.MaxValue;
            var count = 0;
            while (true)
            {
                count++;
                stars.ForEach(s => s.Move(true));
                var tmp = Math.Abs(stars.Max(s => s.Y) - stars.Min(s => s.Y));
                if (tmp > minDiff) break;
                minDiff = tmp;
            }

            Console.WriteLine("Second count = " + --count);
        }

        internal static void Print(List<Star> stars)
        {
            var minX = stars.Min(s => s.X);
            var maxX = stars.Max(s => s.X);
            var minY = stars.Min(s => s.Y);
            var maxY = stars.Max(s => s.Y);
            for (var y = minY; y <= maxY; y++)
            {
                var line = "";
                for (var x = minX; x <= maxX; x++)
                {
                    line += stars.Any(s => s.X == x && s.Y == y) ? '#' : '.';
                }
                Console.WriteLine(line);
            }
        }
    }

    internal class Star
    {
        internal int X;
        internal int Y;
        internal int XVelocity;
        internal int YVelocity;

        internal void Move(bool positive)
        {
            X += positive ? XVelocity : -XVelocity;
            Y += positive ? YVelocity : -YVelocity;
        }
    }
}
