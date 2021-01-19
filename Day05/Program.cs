using System;
using System.IO;

namespace Day05
{
    internal class Program
    {
        private static void Main()
        {
            var s = SolvePart1();
            SolvePart2(s);
        }

        private static string SolvePart1()
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
            return data;
        }

        private static void SolvePart2(string data)
        {
            var safeData = data;

            var minSize = int.MaxValue;

            for (var r = 'a'; r <= 'z'; r++)
            {
                data = new string(safeData);
                data = data.Replace(r.ToString(), "");
                data = data.Replace(char.ToUpper(r).ToString(), "");
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

                minSize = Math.Min(minSize, data.Length);
            }

            Console.WriteLine("Solution is " + minSize);
        }
    }
}
