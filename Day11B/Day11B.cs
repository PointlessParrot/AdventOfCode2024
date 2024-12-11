// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Collections.Concurrent;

namespace Day11A
{
    internal class Day11A
    {
        static bool StoneCheck(List<string> stones, int index, ConcurrentDictionary<string, string[]> cache)
        {
            string stone = stones[index];
            if (stone == "0")
            {
                stones[index] = "1";
                return false;
            }

            if (cache.TryGetValue(stone, out string[] values))
            { 
                stones[index] = values[0];
                if (values.Length == 1) return false;
                stones.Insert(index + 1, values[1]);
                return true;
            }
            
            if (stone.Length % 2 == 0)
            {
                int len = stone.Length / 2;
                stones[index] = stone.Substring(len).TrimStart();
                stones.Insert(index, stone.Substring(0, len));

                cache.TryAdd(stone, new[] { stones[index], stones[index + 1] });
                return true;
            }
            
            stones[index] = (long.Parse(stone) * 2024).ToString();
            
            cache.TryAdd(stone, new[] { stones[index] });
            return false;
        }
        
        static void Blink(List<string> stones, ConcurrentDictionary<string, string[]> cache)
        {
            for (int i = 0; i < stones.Count; i++)
            {
                if (StoneCheck(stones, i, cache)) i++;
            }
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<string> stones = lines[0].Split(' ').ToList();
            ConcurrentDictionary<string, string[]> cache = new ConcurrentDictionary<string, string[]>();
            int iterations = 75;

            for (int i = 0; i < iterations; i++)
            {
                Blink(stones, cache);
                Console.WriteLine(i + 1);
            }

            Console.WriteLine(stones.Count);
        }
    }
}