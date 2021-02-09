using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
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
            var first = 0;
            var second = 1;
            var recipes = new List<int> { 3, 7 };
            var num = int.Parse(input);
            while (recipes.Count < num + 10)
            {
                var sum = (recipes[first] + recipes[second]).ToString();
                recipes.AddRange(sum.Select(c => int.Parse(c.ToString())));
                first += (1 + recipes[first]);
                first %= recipes.Count;
                second += (1 + recipes[second]);
                second %= recipes.Count;
            }

            var output = "";

            for (var i = num; i < num + 10; i++)
            {
                output += recipes[i];
            }
            Console.WriteLine("Result = " + output);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var first = 0;
            var second = 1;
            var recipes = new List<int> { 3, 7 };
            var num = input;
            while (true)
            {
                var sum = (recipes[first] + recipes[second]).ToString();
                recipes.AddRange(sum.Select(c => int.Parse(c.ToString())));
                first += (1 + recipes[first]);
                first %= recipes.Count;
                second += (1 + recipes[second]);
                second %= recipes.Count;
                var val = Check(recipes.TakeLast(7).ToList(), num);
                if (val == -1) continue;
                val += recipes.Count - 7;
                Console.Write(val);
                break;

            }
        }

        internal static int Check(List<int> recipes, string num)
        {
            var found = false;
            var lastI = 0;
            for (var i = 0; i < recipes.Count - num.Length; i++)
            {
                lastI = i;
                var h = i;
                found = true;
                foreach (var c in num)
                {
                    if (recipes[h] != int.Parse(c.ToString()))
                    {
                        found = false;
                        break;
                    }

                    h++;
                }

                if (found) break;
            }

            return found ? lastI : -1;
        }
    }
}
