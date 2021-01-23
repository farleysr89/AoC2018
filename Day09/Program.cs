using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
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
            var parts = data[0].Split(" ");
            var numPlayers = int.Parse(parts[0]);
            var players = new List<Player>();
            for (var i = 0; i < numPlayers; i++)
            {
                players.Add(new Player());
            }
            var lastMarble = int.Parse(parts[6]);
            var currMarble = new Marble { Score = 0 };
            currMarble.Next = currMarble;
            currMarble.Previous = currMarble;
            var currScore = 1;
            while (currScore <= lastMarble)
            {
                foreach (var p in players)
                {
                    if (currScore % 23 == 0)
                    {
                        p.Score += currScore;
                        var tmp = currMarble.Previous.Previous.Previous.Previous.Previous.Previous.Previous;
                        var tmp2 = tmp.Previous;
                        var tmp3 = tmp.Next;
                        p.Score += tmp.Score;
                        tmp2.Next = tmp3;
                        tmp3.Previous = tmp2;
                        currMarble = tmp3;
                    }
                    else
                    {
                        var tmp4 = currMarble.Next;
                        var tmp5 = tmp4.Next;
                        tmp4.Next = new Marble { Score = currScore, Previous = tmp4, Next = tmp5 };
                        tmp4.Previous = currMarble;
                        currMarble = tmp4.Next;
                    }
                    currScore++;
                    if (currScore > lastMarble) break;
                }
            }

            Console.WriteLine(players.Max(p => p.Score));
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }

        internal static void PrintMarbles(Marble root, int count)
        {
            var s = "";
            var tmp = root;
            for (var i = 0; i < count; i++)
            {
                s += tmp.Score + ", ";
                tmp = tmp.Next;
            }
            Console.WriteLine(s);
        }
        internal static void PrintMarblesReverse(Marble root, int count)
        {
            var s = "";
            var tmp = root;
            for (var i = 0; i < count; i++)
            {
                s += tmp.Score + ", ";
                tmp = tmp.Previous;
            }
            Console.WriteLine(s);
        }
    }

    internal class Marble
    {
        internal int Score;
        internal Marble Next;
        internal Marble Previous;
    }

    internal class Player
    {
        internal int Score;
    }
}
