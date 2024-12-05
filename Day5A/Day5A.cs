

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day5A
{
    internal class Day5A
    {
        static bool CheckUpdate((int, int)[] rules, int[] update)
        {
            foreach ((int, int) rule in rules)
            {
                if (!update.Contains(rule.Item1) || !update.Contains(rule.Item2)) continue;
                if (Array.IndexOf(update, rule.Item1) < Array.IndexOf(update, rule.Item2)) continue;
                return false;
            }
            return true;
        }

        static int Middle(int[] update)
        {
            return update[(update.Length - 1)/2];
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int index = Array.FindIndex(lines, line => line == string.Empty);
            (int, int)[] rules = lines.Take(index).Select(line => line.Split('|')).Select(line => (int.Parse(line[0]), int.Parse(line[1]))).ToArray();
            int[][] updates = lines.Skip(index + 1).Select(line => line.Split(',').Select(int.Parse).ToArray()).ToArray();
            updates = updates.Where(x => CheckUpdate(rules, x)).ToArray();
            int total = updates.Sum(Middle);
            
            Console.WriteLine(total);
        }
    }
}