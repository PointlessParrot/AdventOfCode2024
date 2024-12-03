

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day2A
{
    internal class Day2A
    {
        static bool IsSafe(string input)
        {
            int[] line = input.Split(' ').Select(int.Parse).ToArray();
            bool increase = line[1] - line[0] > 0;
            for (int j = 0; j < line.Length - 1; j++)
            {
                if (increase)
                {
                    if (line[j + 1] - line[j] < 0) 
                        return false; 
                    if (line[j + 1] - line[j] < 1)
                        return false; 
                    if (line[j + 1] - line[j] > 3)
                        return false;
                }
                else
                {
                    if (line[j + 1] - line[j] > 0)
                        return false;
                    if (line[j + 1] - line[j] > -1)
                        return false;
                    if (line[j + 1] - line[j] < -3)
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