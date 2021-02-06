using System;
using System.IO;
using System.Linq;

namespace Day13
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
            Console.WriteLine("");
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }
    }

    internal class MineCart
    {
        internal int X;
        internal int Y;
        internal Dir CurrDir;
        internal Turn NextTurn;

        internal enum Dir
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        internal enum Turn
        {
            Left = 0,
            Straight = 1,
            Right = 2
        }
    }
}
