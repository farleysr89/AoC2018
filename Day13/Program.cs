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
            var i = 0;
            foreach (var s in data)
            {
                x = 0;
                var line = "";
                foreach (var c in s)
                {
                    if (carts.Contains(c))
                    {
                        mineCarts.Add(new MineCart
                        { CurrDir = (MineCart.Dir)c, NextTurn = MineCart.Turn.Left, X = x, Y = y, Id = i });
                        i++;
                        switch (c)
                        {
                            case '^':
                            case 'v':
                                line += '|';
                                break;
                            case '<':
                            case '>':
                                line += '-';
                                break;
                            default:
                                Console.WriteLine("Something Broke!");
                                break;
                        }
                    }
                    else line += c;
                    x++;
                }
                map.Add(line);
                y++;
            }

            var turns = 0;
            while (true)
            {
                mineCarts.OrderBy(mc => mc.Y).ThenBy(mcc => mcc.X);
                foreach (var mc in mineCarts)
                {
                    mc.Move(map);
                    if (DetectCollision(mineCarts))
                        break;
                }
                if (DetectCollision(mineCarts))
                    break;

                turns++;
            }

            var loc = CollisionLocation(mineCarts);
            Console.WriteLine("Collision Location = " + loc.Item1 + "," + loc.Item2);
        }

        internal static bool DetectCollision(List<MineCart> mineCarts)
        {
            return mineCarts.Any(mc => mineCarts.Any(mcc => mcc.X == mc.X && mcc.Y == mc.Y && mcc.Id != mc.Id));
        }
        internal static (int, int) CollisionLocation(List<MineCart> mineCarts)
        {
            return mineCarts.First(mc => mineCarts.Any(mcc => mcc.X == mc.X && mcc.Y == mc.Y && mcc.Id != mc.Id)).Location;
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
        internal int Id;
        internal (int, int) Location => (X, Y);

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

        internal void Move(List<string> map)
        {
            switch (CurrDir)
            {
                case Dir.East:
                    X++;
                    if (map[Y][X] == '/')
                        CurrDir = Dir.North;
                    else if (map[Y][X] == '\\')
                        CurrDir = Dir.South;
                    else if (map[Y][X] == '+')
                    {
                        if (NextTurn == Turn.Left)
                        {
                            CurrDir = Dir.North;
                            NextTurn = Turn.Straight;
                        }
                        else if (NextTurn == Turn.Straight)
                        {
                            NextTurn = Turn.Right;
                        }
                        else if (NextTurn == Turn.Right)
                        {
                            CurrDir = Dir.South;
                            NextTurn = Turn.Left;
                        }
                    }
                    else if (map[Y][X] != '-')
                        Console.WriteLine("Something Broke!");
                    break;
                case Dir.West:
                    X--;
                    if (map[Y][X] == '/')
                        CurrDir = Dir.South;
                    else if (map[Y][X] == '\\')
                        CurrDir = Dir.North;
                    else if (map[Y][X] == '+')
                    {
                        if (NextTurn == Turn.Left)
                        {
                            CurrDir = Dir.South;
                            NextTurn = Turn.Straight;
                        }
                        else if (NextTurn == Turn.Straight)
                        {
                            NextTurn = Turn.Right;
                        }
                        else if (NextTurn == Turn.Right)
                        {
                            CurrDir = Dir.North;
                            NextTurn = Turn.Left;
                        }
                    }
                    else if (map[Y][X] != '-')
                        Console.WriteLine("Something Broke!");
                    break;
                case Dir.North:
                    Y--;
                    if (map[Y][X] == '/')
                        CurrDir = Dir.East;
                    else if (map[Y][X] == '\\')
                        CurrDir = Dir.West;
                    else if (map[Y][X] == '+')
                    {
                        if (NextTurn == Turn.Left)
                        {
                            CurrDir = Dir.West;
                            NextTurn = Turn.Straight;
                        }
                        else if (NextTurn == Turn.Straight)
                        {
                            NextTurn = Turn.Right;
                        }
                        else if (NextTurn == Turn.Right)
                        {
                            CurrDir = Dir.East;
                            NextTurn = Turn.Left;
                        }
                    }
                    else if (map[Y][X] != '|')
                        Console.WriteLine("Something Broke!");
                    break;
                case Dir.South:
                    Y++;
                    if (map[Y][X] == '/')
                        CurrDir = Dir.West;
                    else if (map[Y][X] == '\\')
                        CurrDir = Dir.East;
                    else if (map[Y][X] == '+')
                    {
                        if (NextTurn == Turn.Left)
                        {
                            CurrDir = Dir.East;
                            NextTurn = Turn.Straight;
                        }
                        else if (NextTurn == Turn.Straight)
                        {
                            NextTurn = Turn.Right;
                        }
                        else if (NextTurn == Turn.Right)
                        {
                            CurrDir = Dir.West;
                            NextTurn = Turn.Left;
                        }
                    }
                    else if (map[Y][X] != '|')
                        Console.WriteLine("Something Broke!");
                    break;
                default:
                    Console.WriteLine("Something Broke!");
                    break;
            }
        }
    }
}
