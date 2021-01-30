using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
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
            var data = int.Parse(input);
            //data = 18;
            const int minX = 1;
            const int minY = 1;
            const int maxX = 300;
            const int maxY = 300;
            var maxPower = int.MinValue;
            var index = (0, 0);
            var nodes = new Dictionary<(int, int), int>();
            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    nodes.Add((x, y), Power(x, y, data));
                }
            }
            for (var x = minX; x <= maxX - 2; x++)
            {
                for (var y = minY; y <= maxY - 2; y++)
                {
                    var power = nodes.Where(n =>
                            n.Key.Item1 >= x && n.Key.Item1 <= x + 2 && n.Key.Item2 >= y && n.Key.Item2 <= y + 2)
                        .Sum(n => n.Value);
                    if (power <= maxPower) continue;
                    maxPower = power;
                    index = (x, y);
                }
            }
            Console.WriteLine("Maximum power = " + maxPower);
            Console.WriteLine("Index = " + index.Item1 + " " + index.Item2);
            //Console.Write(nodes.Where(n => n.Key.Item1 >= 33 && n.Key.Item1 <= 33 + 2 && n.Key.Item2 >= 45 && n.Key.Item2 <= 45 + 2).Sum(n => n.Value));
        }

        internal static int Power(int x, int y, int grid)
        {
            var rackId = x + 10;
            var power = y * rackId;
            power += grid;
            power *= rackId;
            var powerString = power.ToString();
            var chars = powerString.ToCharArray();
            Array.Reverse(chars);
            powerString = new string(chars);
            power = powerString.Length > 2 ? int.Parse(powerString[2].ToString()) : 0;
            power -= 5;

            return power;
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }
    }
}
