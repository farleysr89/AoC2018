using System;
using System.IO;
using System.Linq;

namespace Day05
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
            var data = input.Split('\n')[0];
            var change = true;
            while (change)
            {
                change = false;
                var tmp = "";
                for (var i = 0; i < data.Length; i++)
                {
                    var c = data[i];
                    if (i == data.Length - 1)
                    {
                        tmp += c;
                        break;
                    }

                    var cc = data[i + 1];
                    if (c != cc && (char.ToLower(c)) == char.ToLower(cc))
                    {
                        i++;
                        change = true;
                    }
                    else
                    {
                        tmp += c;
                    }
                }

                data = new string(tmp);
            }
            Console.WriteLine("Solution is " + data.Length);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }
    }
}
