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
                    "#G......#\n" +
                    "#.E.#...#\n" +
                    "#..##..G#\n" +
                    "#...##..#\n" +
                    "#...#...#\n" +
                    "#.G...G.#\n" +
                    "#########\n").Split('\n').ToList();
            var y = 0;
            var id = 0;
            var fighters = new List<Fighter>();
            foreach (var l in data)
            {
                var x = 0;
                foreach (var c in l)
                {
                    if (c == 'E' || c == 'G')
                    {
                        fighters.Add(new Fighter
                        {
                            Id = id,
                            X = x,
                            Y = y,
                            IsElf = c == 'E'
                        });
                        id++;
                    }
                    x++;
                }
                y++;
            }
            var sortedFighters = fighters.OrderBy(f => f.Y).ThenBy(f => f.X).ToList();
            var rounds = -1;
            while (fighters.Any(f => f.IsElf) && fighters.Any(f => !f.IsElf))
            {
                rounds++;
                foreach (var f in sortedFighters)
                {
                    if(!(fighters.Any(ff => ff.IsElf) && fighters.Any(ff => !ff.IsElf))) break;
                    if (fighters.Where(ff => f.IsElf ? !ff.IsElf : ff.IsElf).Any(ff => LegalFight(f, ff)))
                    {
                        var closeOpponents = fighters.Where(ff => f.IsElf ? !ff.IsElf : ff.IsElf).Where(ff => LegalFight(f, ff)).ToList();
                        var min = closeOpponents.Min(oo => oo.Health);
                        closeOpponents = closeOpponents.Where(o => o.Health == min).ToList();
                        if (closeOpponents.Count() == 1)
                        {
                            var opponent = closeOpponents.First();
                            fighters.First(ff => ff.Id == opponent.Id).Health -= f.Damage;
                            fighters.RemoveAll(ff => ff.Health <= 0);
                        }
                        else
                        {
                            closeOpponents.OrderBy(o => o.Y).ThenBy(o => o.X);
                            var opponent = closeOpponents.First();
                            fighters.First(ff => ff.Id == opponent.Id).Health -= f.Damage;
                            fighters.RemoveAll(ff => ff.Health <= 0);
                        }
                    }
                    else
                    {
                        var cells = GetDistances(f, data, fighters).ToList();
                        var dest = FindMove(f, fighters, cells);
                        if (dest == null) continue;
                        var move = FindPath(dest, cells);
                        fighters.First(ff => ff.Id == f.Id).X = move.X;
                        fighters.First(ff => ff.Id == f.Id).Y = move.Y;
                        
                        if (fighters.Where(ff => f.IsElf ? !ff.IsElf : ff.IsElf).Any(ff => LegalFight(f, ff)))
                        {
                            var closeOpponents = fighters.Where(ff => f.IsElf ? !ff.IsElf : ff.IsElf).Where(ff => LegalFight(f, ff)).ToList();
                            var min = closeOpponents.Min(oo => oo.Health);
                            closeOpponents = closeOpponents.Where(o => o.Health == min).ToList();
                            if (closeOpponents.Count() == 1)
                            {
                                var opponent = closeOpponents.First();
                                fighters.First(ff => ff.Id == opponent.Id).Health -= f.Damage;
                                fighters.RemoveAll(ff => ff.Health <= 0);
                            }
                            else
                            {
                                closeOpponents.OrderBy(o => o.Y).ThenBy(o => o.X);
                                var opponent = closeOpponents.First();
                                fighters.First(ff => ff.Id == opponent.Id).Health -= f.Damage;
                                fighters.RemoveAll(ff => ff.Health <= 0);
                            }
                        }
                    }
                }

                sortedFighters = fighters.OrderBy(f => f.Y).ThenBy(f => f.X).ToList();
            }

            Console.WriteLine("Final result = " + (rounds * fighters.Sum(f => f.Health)));
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
            var closestOpponents = opponents.Select(o => GetDistance(o, cells));
            return !closestOpponents.Any(c => c != null) ? null : closestOpponents.Where(c => c != null).OrderBy(c => c.Distance).ThenBy(c => c.Y).ThenBy(c => c.X).First();
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

        private static Cell FindPath(Cell c, IReadOnlyCollection<Cell> cells)
        {
            var distance = c.Distance;
            while (distance > 1)
            {
                distance--;
                c = cells.Where(cc => cc.Distance == distance && LegalMove(c, cc)).OrderBy(cc=> cc.Y).ThenBy(cc => cc.X).First();
                
            }
            return c;
        }

        private static bool LegalMove(Cell c, Cell cc)
        {
            var moves = new List<(int,int)>{ (0, -1), (-1, 0), (1, 0), (0, 1) };
            return (from m in moves let x = c.X + m.Item1 let y = c.Y + m.Item2 where x == cc.X && y == cc.Y select x).Any();
        }
        private static bool LegalFight(Fighter f, Fighter ff)
        {
            var moves = new List<(int,int)>{ (0, -1), (-1, 0), (1, 0), (0, 1) };
            foreach (var m in moves)
            {
                var x = f.X + m.Item1;
                var y = f.Y + m.Item2;
                if (x == ff.X && y == ff.Y) return true;
            }

            return false;
            //return (from m in moves let x = f.X + m.Item1 let y = f.Y + m.Item2 where x == ff.X && y == ff.Y select x).Any();
        }
    }

    internal class Fighter
    {
        internal int Id;
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
