using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Day15
{
    internal static class Program
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
            data = ("#########\n" +
                    "#G..G..G#\n" +
                    "#.......#\n" +
                    "#.......#\n" +
                    "#G..E..G#\n" +
                    "#.......#\n" +
                    "#.......#\n" +
                    "#G..G..G#\n" +
                    "#########\n").Split('\n').ToList();
            var y = 0;
            var fighters = new List<Fighter>();
            foreach (var l in data)
            {
                var x = 0;
                foreach (var c in l)
                {
                    if (c == 'E' || c == 'G')
                        fighters.Add(new Fighter
                        {
                            X = x,
                            Y = y,
                            IsElf = c == 'E'
                        });
                    x++;
                }
                y++;
            }
            var sortedFighters = fighters.OrderBy(f => f.Y).ThenBy(f => f.X);
            var rounds = 0;
            while (!sortedFighters.All(f => f.IsElf) || !sortedFighters.All(f => !f.IsElf))
            {
                rounds++;
                foreach (var f in sortedFighters)
                {
                    var cells = GetDistances(f, data, fighters);
                    var dest = FindMove(f, fighters, cells.ToList());
                    // var (item1, item2) = FindMove(f, data, sortedFighters);
                    // f.X = item1;
                    // f.Y = item2;
                }
            }

            Console.WriteLine("");
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }

        private static bool CanMove(int destX, int destY, IReadOnlyList<string> map, IEnumerable<Fighter> fighters)
        {
            if (map[destY][destX] == '#') return false;
            return !fighters.Any(ff => ff.X == destX && ff.Y == destY);
        }

        private static Cell FindMove(Fighter f, IEnumerable<Fighter> fighters, IReadOnlyCollection<Cell> cells)
        {
            var opponents = fighters.Where(ff => f.IsElf ? !ff.IsElf : ff.IsElf);
            var closestOpponents = opponents.Select(o => GetDistance(o, cells)).OrderBy(c => c.Distance).ThenBy(c => c.Y).ThenBy(c => c.X);
            return closestOpponents.First();

        }

        private static Cell GetDistance(Fighter f,  IReadOnlyCollection<Cell> cells)
        {
            var moves = new List<(int,int)>{ (0, -1), (-1, 0), (1, 0), (0, 1) };
            var distance = int.MaxValue;
            Cell returnC = null;
            foreach (var m in moves)
            {
                var x = f.X + m.Item1;
                var y = f.Y + m.Item2;
                var c = cells.FirstOrDefault(cc => cc.X == x && cc.Y == y);
                if (c == null || c.Distance >= distance) continue;
                distance = c.Distance;
                returnC = c;
            }
            return returnC;
        }        
        
        private static IEnumerable<Cell> GetDistances(Fighter f, IReadOnlyList<string> map, IEnumerable<Fighter> fighters)
        {
            var cells = new List<Cell>();
            var moves = new List<(int,int)>{ (0, -1), (-1, 0), (1, 0), (0, 1) };
            var distance = 1;
            cells.Add(new Cell
            {
                Distance = 0,
                X = f.X,
                Y = f.Y
            });
            cells.AddRange(from m in moves let x = f.X + m.Item1 let y = f.Y + m.Item2 where CanMove(x, y, map, fighters) select new Cell { Distance = distance, X = x, Y = y });
            while (true)
            {
                distance++;
                var count = 0;
                var tempCells = cells.Select(c => new Cell
                {
                    X = c.X,
                    Y = c.Y,
                    Distance = distance
                }).ToList();
                foreach (var c in tempCells)
                {
                    foreach (var (item1, item2) in moves)
                    {
                        var x = c.X + item1;
                        var y = c.Y + item2;
                        var cc = cells.FirstOrDefault(ccc => ccc.X == x && ccc.Y == y);
                        if (cc != null && cc.Distance <= distance || !CanMove(x, y, map, fighters))
                        {
                            continue;
                        }

                        count++;

                        if (cc == null)
                            cells.Add(new Cell
                            {
                                X = x,
                                Y = y,
                                Distance = distance
                            });
                        else
                            cc.Distance = distance;
                    }
                }

                if (count == 0) break;
            }
            
            return cells;
        }
    }

    internal class Fighter
    {
        internal int X;
        internal int Y;
        internal bool IsElf;
        internal int Health = 200;
        internal int Damage = 3;
    }

    internal class Cell
    {
        internal int X;
        internal int Y;
        internal int Distance;
    }
}
