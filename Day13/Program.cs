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
            var y = 0;
            var mineCarts = new List<MineCart>();
            var i = 0;
            foreach (var s in data)
            {
                var x = 0;
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
            }

            var (item1, item2) = CollisionLocation(mineCarts);
            Console.WriteLine("Collision Location = " + item1 + "," + item2);
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
            char[] carts = { '>', '<', '^', 'v' };
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var map = new List<string>();
            var y = 0;
            var mineCarts = new List<MineCart>();
            var i = 0;
            foreach (var s in data)
            {
                var x = 0;
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

            while (mineCarts.Count > 1)
            {
                mineCarts.OrderBy(mc => mc.Y).ThenBy(mcc => mcc.X);
                var tmpCarts = new List<MineCart>(mineCarts);
                foreach (var mc in mineCarts)
                {
                    mc.Move(map);
                    if (!DetectCollision(mineCarts)) continue;
                    var (cx, cy) = CollisionLocation(mineCarts);
                    tmpCarts.RemoveAll(mcc => mcc.X == cx && mcc.Y == cy);
                }

                mineCarts = new List<MineCart>(tmpCarts);
            }

            var (item1, item2) = mineCarts.First().Location;
            Console.WriteLine("Last mine cart Location = " + item1 + "," + item2);
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
                    switch (map[Y][X])
                    {
                        case '/':
                            CurrDir = Dir.North;
                            break;
                        case '\\':
                            CurrDir = Dir.South;
                            break;
                        case '+' when NextTurn == Turn.Left:
                            CurrDir = Dir.North;
                            NextTurn = Turn.Straight;
                            break;
                        case '+' when NextTurn == Turn.Straight:
                            NextTurn = Turn.Right;
                            break;
                        case '+':
                            {
                                if (NextTurn == Turn.Right)
                                {
                                    CurrDir = Dir.South;
                                    NextTurn = Turn.Left;
                                }

                                break;
                            }
                        default:
                            {
                                if (map[Y][X] != '-')
                                    Console.WriteLine("Something Broke!");
                                break;
                            }
                    }
                    break;
                case Dir.West:
                    X--;
                    switch (map[Y][X])
                    {
                        case '/':
                            CurrDir = Dir.South;
                            break;
                        case '\\':
                            CurrDir = Dir.North;
                            break;
                        case '+' when NextTurn == Turn.Left:
                            CurrDir = Dir.South;
                            NextTurn = Turn.Straight;
                            break;
                        case '+' when NextTurn == Turn.Straight:
                            NextTurn = Turn.Right;
                            break;
                        case '+':
                            {
                                if (NextTurn == Turn.Right)
                                {
                                    CurrDir = Dir.North;
                                    NextTurn = Turn.Left;
                                }

                                break;
                            }
                        default:
                            {
                                if (map[Y][X] != '-')
                                    Console.WriteLine("Something Broke!");
                                break;
                            }
                    }
                    break;
                case Dir.North:
                    Y--;
                    switch (map[Y][X])
                    {
                        case '/':
                            CurrDir = Dir.East;
                            break;
                        case '\\':
                            CurrDir = Dir.West;
                            break;
                        case '+' when NextTurn == Turn.Left:
                            CurrDir = Dir.West;
                            NextTurn = Turn.Straight;
                            break;
                        case '+' when NextTurn == Turn.Straight:
                            NextTurn = Turn.Right;
                            break;
                        case '+':
                            {
                                if (NextTurn == Turn.Right)
                                {
                                    CurrDir = Dir.East;
                                    NextTurn = Turn.Left;
                                }

                                break;
                            }
                        default:
                            {
                                if (map[Y][X] != '|')
                                    Console.WriteLine("Something Broke!");
                                break;
                            }
                    }
                    break;
                case Dir.South:
                    Y++;
                    switch (map[Y][X])
                    {
                        case '/':
                            CurrDir = Dir.West;
                            break;
                        case '\\':
                            CurrDir = Dir.East;
                            break;
                        case '+' when NextTurn == Turn.Left:
                            CurrDir = Dir.East;
                            NextTurn = Turn.Straight;
                            break;
                        case '+' when NextTurn == Turn.Straight:
                            NextTurn = Turn.Right;
                            break;
                        case '+':
                            {
                                if (NextTurn == Turn.Right)
                                {
                                    CurrDir = Dir.West;
                                    NextTurn = Turn.Left;
                                }

                                break;
                            }
                        default:
                            {
                                if (map[Y][X] != '|')
                                    Console.WriteLine("Something Broke!");
                                break;
                            }
                    }
                    break;
                default:
                    Console.WriteLine("Something Broke!");
                    break;
            }
        }
    }
}
