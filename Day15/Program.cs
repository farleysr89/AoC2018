using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    var (item1, item2) = FindMove(f, data, sortedFighters);
                    f.X = item1;
                    f.Y = item2;
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

        private static (int,int) FindMove(Fighter f, IReadOnlyList<string> map, IEnumerable<Fighter> fighters)
        {
            var opponents = fighters.Where(ff => f.IsElf ? !ff.IsElf : ff.IsElf);
            var closestOpponents = opponents.OrderBy(o => GetDistance(f, o)).ThenBy(o => o.Y).ThenBy(o => o.X);
            return (0, 0);

        }

        private static int GetDistance(Fighter f, Fighter ff, IReadOnlyList<string> map = null, IEnumerable<Fighter> fighters = null)
        {
            return Math.Abs(f.X - ff.X) + Math.Abs(f.Y - ff.Y);
        }        
        
        private static int GetDistances(Fighter f, IReadOnlyList<string> map = null, IEnumerable<Fighter> fighters = null)
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
            
            
            return 0;
        }
        
        private static int GetDistance(Fighter f, int destX, int destY, IReadOnlyList<string> map = null, IEnumerable<Fighter> fighters = null)
        {
            
            return 0;
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
