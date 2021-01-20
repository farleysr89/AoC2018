using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
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
            var coordinates = new List<Coordinate>();
            var id = 'A';
            foreach (var s in data.Where(s => s != ""))
            {
                var parts = s.Split(",").Select(int.Parse).ToList();
                coordinates.Add(new Coordinate { X = parts[0], Y = parts[1], Id = id });
                if (id == 'Z') id = 'a';
                else id++;
            }
            var minX = coordinates.Min(c => c.X);
            var minY = coordinates.Min(c => c.Y);
            coordinates.ForEach(c => c.X -= minX);
            coordinates.ForEach(c => c.Y -= minY);
            var maxX = coordinates.Max(c => c.X);
            var maxY = coordinates.Max(c => c.Y);
            var map = new List<List<char>>();
            for (var y = 0; y <= maxY; y++)
            {
                map.Add(new List<char>());
                for (var x = 0; x <= maxX; x++)
                {
                    var min = coordinates.Min(c => c.Distance(x, y));
                    var cc = coordinates.Count(c => c.Distance(x, y) == min) > 1
                        ? '.'
                        : coordinates.First(c => c.Distance(x, y) == min).Id;
                    map[y].Add(cc);
                }
            }

            var skip = new HashSet<char>();
            foreach (var c in map[0]) skip.Add(c);
            foreach (var c in map.Last()) skip.Add(c);
            foreach (var r in map)
            {
                skip.Add(r[0]);
                skip.Add(r.Last());
            }

            var maxSize = coordinates.Where(c => !skip.Contains(c.Id)).Select(c => map.Sum(m => m.Count(mm => mm == c.Id))).Prepend(int.MinValue).Max();
            Console.WriteLine("Max size = " + maxSize);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var coordinates = new List<Coordinate>();
            var id = 'A';
            foreach (var s in data.Where(s => s != ""))
            {
                var parts = s.Split(",").Select(int.Parse).ToList();
                coordinates.Add(new Coordinate { X = parts[0], Y = parts[1], Id = id });
                if (id == 'Z') id = 'a';
                else id++;
            }
            var minX = coordinates.Min(c => c.X);
            var minY = coordinates.Min(c => c.Y);
            coordinates.ForEach(c => c.X -= minX);
            coordinates.ForEach(c => c.Y -= minY);
            var maxX = coordinates.Max(c => c.X);
            var maxY = coordinates.Max(c => c.Y);

            var locationCount = 0;
            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    if (coordinates.Sum(c => c.Distance(x, y)) < 10000) locationCount++;
                }
            }
            Console.WriteLine("Location Count = " + locationCount);
        }
    }
    internal class Coordinate
    {
        internal int X;
        internal int Y;
        internal char Id;

        internal int Distance(int x, int y)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y);
        }
    }
}
