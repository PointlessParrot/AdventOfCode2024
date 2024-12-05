// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day5B
{
    internal class Day5B
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

        static int[] FixUpdate((int, int)[] rules, int[] update)
        {
            List<int> output = new List<int>();
            rules = rules.Where(r => update.Contains(r.Item1) && update.Contains(r.Item2)).ToArray();
            foreach (int item in update)
            {
                (int, int)[] rulesShort = rules.Where(r => r.Item1 == item).ToArray();
                int[] indexes = rulesShort.Select(x => output.IndexOf(x.Item2)).Where(x => x != -1).ToArray();
                if (indexes.Length == 0)
                {
                    output.Add(item);
                    continue;
                }  
                output.Insert(indexes.Min(), item);
            }
            Console.WriteLine(string.Join(", ", output.ToArray()));
            return output.ToArray();
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int index = Array.FindIndex(lines, line => line == string.Empty);
            (int, int)[] rules = lines.Take(index).Select(line => line.Split('|')).Select(line => (int.Parse(line[0]), int.Parse(line[1]))).ToArray();
            int[][] updates = lines.Skip(index + 1).Select(line => line.Split(',').Select(int.Parse).ToArray()).ToArray();
            updates = updates.Where(x => !CheckUpdate(rules, x)).ToArray();
            updates = updates.Select(x => FixUpdate(rules, x)).ToArray();
            int total = updates.Sum(Middle);
            
            Console.WriteLine(total);
        }
    }
}