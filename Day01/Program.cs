using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
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
            var frequency = data.Where(s => s != "").Sum(int.Parse);
            Console.WriteLine("Final frequency is " + frequency);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var visited = new HashSet<int> { 0 };
            var frequency = 0;
            while (true)
            {
                foreach (var s in data.Where(s => s != ""))
                {
                    frequency += int.Parse(s);
                    if (visited.Contains(frequency))
                    {
                        Console.WriteLine("Final frequency is " + frequency);
                        return;
                    }

                    visited.Add(frequency);
                }
            }
        }
    }
}
