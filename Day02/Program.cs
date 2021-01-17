using System;
using System.IO;
using System.Linq;

namespace Day02
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
            var twos = 0;
            var threes = 0;
            foreach (var s in data.Where(s => s != ""))
            {
                var twoMatch = false;
                var threeMatch = false;
                var newS = string.Concat(s.OrderBy(c => c));
                var prev = newS[0];
                var count = 1;
                foreach (var c in newS[1..])
                {
                    if (c == prev) count++;
                    else
                    {
                        switch (count)
                        {
                            case 2:
                                twoMatch = true;
                                break;
                            case 3:
                                threeMatch = true;
                                break;
                        }

                        count = 1;
                        prev = c;
                    }
                }
                switch (count)
                {
                    case 2:
                        twoMatch = true;
                        break;
                    case 3:
                        threeMatch = true;
                        break;
                }

                if (twoMatch) twos++;
                if (threeMatch) threes++;
            }
            Console.WriteLine("Checksum is " + (twos * threes));
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var first = "";
            var index = 0;
            foreach (var s in data.Where(s => s != ""))
            {
                foreach (var ss in data.GetRange(data.IndexOf(s), data.Count - data.IndexOf(s)).Where(sss => sss != ""))
                {
                    var difCount = 0;
                    for (var i = 0; i < s.Length; i++)
                    {
                        if (s[i] != ss[i])
                        {
                            index = i;
                            difCount++;
                        }
                        if (difCount > 1) break;
                    }

                    if (difCount != 1) continue;
                    first = s;
                    break;
                }

                if (first != "") break;
            }
            Console.WriteLine("Common letters are  " + first[0..index] + first[(index + 1)..]);
        }
    }
}
