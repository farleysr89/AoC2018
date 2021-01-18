using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
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
            var map = new List<List<int>>();
            for (var y = 0; y < 1; y++)
            {
                map.Add(new List<int>());
                for (var x = 0; x < 1; x++)
                {
                    map[y].Add(0);
                }
            }

            foreach (var s in data.Where(s => s != ""))
            {
                var parts = s.Split(" @ ");
                parts = parts[1].Split(": ");
                var offsets = parts[0].Split((","));
                var xOffset = int.Parse(offsets[0]);
                var yOffset = int.Parse(offsets[1]);

                var dimensions = parts[1].Split("x");
                var width = int.Parse(dimensions[0]);
                var height = int.Parse(dimensions[1]);
                var xMax = xOffset + width;
                var yMax = yOffset + height;
                if (xMax > map[0].Count)
                {
                    foreach (var l in map)
                    {
                        while (l.Count < xMax) l.Add(0);
                    }
                }

                if (yMax > map.Count)
                {
                    for (var y = map.Count; y < yMax; y++)
                    {
                        map.Add(new List<int>());
                        while (map[y].Count < xMax) map[y].Add(0);
                    }
                }
                for (var x = xOffset; x < xOffset + width; x++)
                {
                    for (var y = yOffset; y < yOffset + height; y++)
                    {
                        map[y][x]++;
                    }
                }
            }

            var count = map.SelectMany(c => c).Count(cc => cc > 1);
            Console.WriteLine("Overlapping square inches = " + count);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var map = new List<List<int>>();
            for (var y = 0; y < 1; y++)
            {
                map.Add(new List<int>());
                for (var x = 0; x < 1; x++)
                {
                    map[y].Add(0);
                }
            }

            var index = 0;
            foreach (var s in data.Where(s => s != ""))
            {
                index++;
                var parts = s.Split(" @ ");
                parts = parts[1].Split(": ");
                var offsets = parts[0].Split((","));
                var xOffset = int.Parse(offsets[0]);
                var yOffset = int.Parse(offsets[1]);

                var dimensions = parts[1].Split("x");
                var width = int.Parse(dimensions[0]);
                var height = int.Parse(dimensions[1]);
                var xMax = xOffset + width;
                var yMax = yOffset + height;
                if (xMax > map[0].Count)
                {
                    foreach (var l in map)
                    {
                        while (l.Count < xMax) l.Add(0);
                    }
                }

                if (yMax > map.Count)
                {
                    for (var y = map.Count; y < yMax; y++)
                    {
                        map.Add(new List<int>());
                        while (map[y].Count < xMax) map[y].Add(0);
                    }
                }
                for (var x = xOffset; x < xOffset + width; x++)
                {
                    for (var y = yOffset; y < yOffset + height; y++)
                    {
                        map[y][x] += index;
                    }
                }
            }

            index = 0;
            var found = false;
            foreach (var s in data.Where(s => s != ""))
            {
                found = true;
                index++;
                var parts = s.Split(" @ ");
                parts = parts[1].Split(": ");
                var offsets = parts[0].Split((","));
                var xOffset = int.Parse(offsets[0]);
                var yOffset = int.Parse(offsets[1]);

                var dimensions = parts[1].Split("x");
                var width = int.Parse(dimensions[0]);
                var height = int.Parse(dimensions[1]);
                for (var x = xOffset; x < xOffset + width; x++)
                {
                    for (var y = yOffset; y < yOffset + height; y++)
                    {
                        if (map[y][x] == index) continue;
                        found = false;
                        break;
                    }
                }

                if (found) break;
            }

            Console.WriteLine("Valid id = " + index);
        }
    }
}
