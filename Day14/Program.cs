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
            var recipes = "37";
            var num = input;
            while (recipes.Length < 20 || !recipes[^10..].Contains(num))
            {
                var sum = (int.Parse(recipes[first].ToString()) + int.Parse(recipes[second].ToString()));
                recipes += sum;
                first += (1 + int.Parse(recipes[first].ToString()));
                first %= recipes.Length;
                second += (1 + int.Parse(recipes[second].ToString()));
                second %= recipes.Length;
            }

            //var output = "";

            //for (var i = num; i < num + 10; i++)
            //{
            //    output += recipes[i];
            //}
            Console.WriteLine("Result = " + (recipes.Length - 5));
        }
    }
}
