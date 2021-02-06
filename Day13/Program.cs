using System;
using System.Collections.Generic;
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
            char[] carts = { '>', '<', '^', 'v' };
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var map = new List<string>();
            var x = 0;
            var y = 0;
            var mineCarts = new List<MineCart>();
            foreach (var s in data)
            {
                x = 0;
                foreach (var c in s)
                {
                    if (carts.Contains(c))
                    {
                        mineCarts.Add(new MineCart { CurrDir = (MineCart.Dir)c, NextTurn = MineCart.Turn.Left, X = x, Y = y });
                    }
                    x++;
                }
                y++;
            }
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
            North = '^',
            East = '>',
            South = 'v',
            West = '<'
        }

        internal enum DirSymbol
        {
            North = '^',
            East = '>',
            South = 'v',
            West = '<'
        }

        internal enum Turn
        {
            Left = 0,
            Straight = 1,
            Right = 2
        }
    }
}
