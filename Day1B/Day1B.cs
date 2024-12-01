using System;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day1B
{
    internal class Day1B
    {
        static void Main(string[] args)
        {
            string[][] lines = System.IO.File.ReadAllLines("input.txt").Select(line => line.Replace("   ","|").Split('|')).ToArray();
            int[] left = lines.Select(l => int.Parse(l[0])).ToArray();
            int[] right = lines.Select(l => int.Parse(l[1])).ToArray();
            int ans = left.Select(x => x * right.Select(y => x == y ? 1: 0).Sum()).Sum();
            Console.WriteLine(ans);
        }
    }
}