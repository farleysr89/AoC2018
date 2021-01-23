using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day08
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
            var data = input.Split(" ").Select(int.Parse).ToList();
            var root = new Node();
            var index = 0;
            var nodeCount = data[index];
            index++;
            var metadataCount = data[index];
            index++;

            for (var i = 0; i < nodeCount; i++)
            {
                var (node, newIndex) = ProcessNode(index, data);
                root.Children.Add(node);
                index = newIndex;
            }

            for (var i = 0; i < metadataCount; i++)
            {
                root.Metadata.Add(data[index + i]);
            }

            Console.WriteLine("Metadata sum = " + root.Sum());
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            Console.WriteLine("");
        }

        internal static (Node, int) ProcessNode(int index, List<int> data)
        {
            var child = new Node();
            var nodeCount = data[index];
            index++;
            var metadataCount = data[index];
            index++;

            for (var i = 0; i < nodeCount; i++)
            {
                var (node, newIndex) = ProcessNode(index, data);
                child.Children.Add(node);
                index = newIndex;
            }

            for (var i = 0; i < metadataCount; i++)
            {
                child.Metadata.Add(data[index + i]);
            }

            index += metadataCount;
            return (child, index);
        }
    }

    internal class Node
    {
        internal List<Node> Children = new List<Node>();
        internal List<int> Metadata = new List<int>();

        internal int Sum()
        {
            return Metadata.Sum() + Children.Select(c => c.Sum()).Sum();
        }
    }
}
