using System;
using System.Collections.Generic;
using System.IO;

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
            const int minX = 1;
            const int minY = 1;
            const int maxX = 300;
            const int maxY = 300;
            var maxPower = long.MinValue;
            var nodes = new Dictionary<(int, int), long>();
            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    nodes.Add((x, y), Power(x, y, data));
                }
            }

            var processed = PreProcess(nodes);
            int bx = 0, by = 0;

            const int s = 3;
            for (var y = s; y <= 300; y++)
            {
                for (var x = s; x <= 300; x++)
                {
                    var total = processed[(x, y)];
                    if (y - s > 0) total -= processed[(x, y - s)];
                    if (x - s > 0) total -= processed[(x - s, y)];
                    if (x - s > 0 && y - s > 0) total += processed[(x - s, y - s)];
                    if (total <= maxPower) continue;
                    maxPower = total;
                    bx = x - s + 1;
                    by = y - s + 1;
                }
            }

            Console.WriteLine("Maximum power = " + maxPower);
            Console.WriteLine(bx + "," + by);
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

        // Code from here: https://www.geeksforgeeks.org/submatrix-sum-queries/
        internal static Dictionary<(int, int), long> PreProcess(Dictionary<(int, int), long> orig)
        {
            var aux = new Dictionary<(int, int), long>();
            // Copy first row of mat[][] to aux[][] 
            for (var i = 1; i <= 300; i++)
                aux[(1, i)] = orig[(1, i)];

            // Do column wise sum 
            for (var i = 2; i <= 300; i++)
                for (var j = 1; j <= 300; j++)
                    aux[(i, j)] = orig[(i, j)] + aux[(i - 1, j)];

            // Do row wise sum 
            for (var i = 1; i <= 300; i++)
                for (var j = 2; j <= 300; j++)
                    aux[(i, j)] += aux[(i, j - 1)];

            return aux;
        }
        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = int.Parse(input);
            const int minX = 1;
            const int minY = 1;
            const int maxX = 300;
            const int maxY = 300;
            var maxPower = long.MinValue;
            var nodes = new Dictionary<(int, int), long>();
            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    nodes.Add((x, y), Power(x, y, data));
                }
            }

            var processed = PreProcess(nodes);
            int bx = 0, by = 0, bs = 0;

            for (var s = 1; s <= 300; s++)
            {
                for (var y = s; y <= 300; y++)
                {
                    for (var x = s; x <= 300; x++)
                    {
                        var total = processed[(x, y)];
                        if (y - s > 0) total -= processed[(x, y - s)];
                        if (x - s > 0) total -= processed[(x - s, y)];
                        if (x - s > 0 && y - s > 0) total += processed[(x - s, y - s)];
                        if (total <= maxPower) continue;
                        maxPower = total;
                        bx = x - s + 1;
                        by = y - s + 1;
                        bs = s;
                    }
                }
            }
            Console.WriteLine("Maximum power = " + maxPower);
            Console.WriteLine(bx + "," + by + "," + bs);
        }
    }
}
