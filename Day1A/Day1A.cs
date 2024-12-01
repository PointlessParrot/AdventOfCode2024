using System;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day1A
{
    internal class Day1A
    {
        static void Main(string[] args)
        {
            string[][] lines = System.IO.File.ReadAllLines("input.txt").Select(line => line.Replace("   ","|").Split('|')).ToArray();
            int[] left = lines.Select(l => int.Parse(l[0])).ToArray();
            int[] right = lines.Select(l => int.Parse(l[1])).ToArray();
            Array.Sort(left);
            Array.Sort(right);
            int[] diffs = new int[left.Length];
            for (int i = 0; i < left.Length; i++) diffs[i] = left[i] - right[i];
            Console.Write(diffs.Select(x => x < 0 ? -x : x).Sum());
        }
    }
}