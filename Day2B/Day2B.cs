// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Net;

namespace Day2A
{
    internal class Day2A
    {
        static bool Check(int a, int b, bool increase)
        {
            bool safe = true;
            if (increase)
            {
                if (b - a < 0) 
                    safe = false; 
                else if (b - a < 1)
                    safe = false; 
                else if (b - a > 3)
                    safe = false;
            }
            else
            {
                if (b - a > 0)
                    safe = false;
                else if (b - a > -1)
                    safe = false;
                else if (b - a < -3)
                    safe = false;
            }
            return safe;
        }
        
        static string Remove(int[] input, int i) => string.Join(' ', input.Take(i)) + ' ' + string.Join(' ', input.Skip(i + 1));
        
        static bool IsSafe(string input, bool removed = false)
        {
            int[] line = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            bool increase = line[1] - line[0] > 0;
            bool safe = true;
            for (int j = 0; j < line.Length - 1; j++)
            {
                if (!Check(line[j], line[j + 1], increase)) safe = false;

                if (!safe)
                {
                    if (removed) return false;

                    if (j - 1 == 0 || j == 0)
                    {
                        if (IsSafe(Remove(line, 0), true)) return true;
                    }

                    if (j + 1 == line.Length - 1)
                    {
                        return true;
                    }

                    if (j > 0)
                    {
                        if (IsSafe(Remove(line, j), true)) return true; 
                    }

                    if (j + 1 < line.Length - 1)
                    {
                        if (IsSafe(Remove(line, j + 1), true)) return true;
                    }
                    
                    return false;
                }
            }

            return true;
        }
        
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            int[] nums = lines.Select(x => IsSafe(x) ? 1 : 0).ToArray();
            Console.WriteLine(nums.Sum());
            for (int i = 0; i < lines.Length; i++)
            {
                if (nums[i] == 1) Console.WriteLine(lines[i]);
            }
        }
    }
}