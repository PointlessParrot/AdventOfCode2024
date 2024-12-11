// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace Day11A
{
    public class ResultCache
    {
        static MemoryCache cache = MemoryCache.Default;

        public static bool TryGetData(string key, out string[] values)
        {
            if (cache.Contains(key))
            {
                values = (string[])cache.Get(key);
                return true;
            }
            
            values = new string[0];
            return false;
        }

        public static void Add(string key, string[] values)
        {
            cache.Add(new CacheItem(key, values), null);
        }
    }
    
    internal class Day11A
    {
        static bool StoneCheck(List<string> stones, int index)
        {
            string stone = stones[index];
            if (stone == "0")
            {
                stones[index] = "1";
                return false;
            }

            if (ResultCache.TryGetData(stone, out string[] values))
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

                ResultCache.Add(stone, new[] { stones[index], stones[index + 1] });
                return true;
            }
            
            stones[index] = (long.Parse(stone) * 2024).ToString();
            
            ResultCache.Add(stone, new[] { stones[index] });
            return false;
        }
        
        static void Blink(List<string> stones)
        {
            for (int i = 0; i < stones.Count; i++)
            {
                if (StoneCheck(stones, i)) i++;
            }
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<string> stones = lines[0].Split(' ').ToList();
            int iterations = 75;

            for (int i = 0; i < iterations; i++)
            {
                Blink(stones);
                Console.WriteLine(i + 1);
            }

            Console.WriteLine(stones.Count);
        }
    }
}