// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace Day11A
{
    public class ResultCache
    {
        static MemoryCache cache = MemoryCache.Default;

        public static bool TryGetData(string stone, int iterationsRemaining, out long count)
        {
            string key = stone + "|" + iterationsRemaining;
            if (cache.Contains(key))
            {
                count = (long)cache.Get(key);
                return true;
            }

            count = -1;
            return false;
        }

        public static void Add(string stone, int iterationsRemaining, long count)
        {
            string key = stone + "|" + iterationsRemaining;
            cache.Add(new CacheItem(key, count), null);
        }
    }
    
    internal class Day11A
    {
        static long IterateAndCount(string stoneInput, int iterationsRemaining)
        {
            if (iterationsRemaining == 0)
                return 1;
            
            if (ResultCache.TryGetData(stoneInput, iterationsRemaining, out long count))
                return count;

            iterationsRemaining--;
            long ans;
            int l = stoneInput.Length;
            
            if (stoneInput == "0" || stoneInput == "")
                ans = IterateAndCount("1", iterationsRemaining);
            
            else if (l % 2 == 0)
                ans = IterateAndCount(stoneInput.Substring(0, l/2), iterationsRemaining) 
                       + IterateAndCount(stoneInput.Substring(l/2).TrimStart('0'), iterationsRemaining);

            else 
                ans = IterateAndCount((long.Parse(stoneInput) * 2024).ToString(), iterationsRemaining);
            
            ResultCache.Add(stoneInput, iterationsRemaining + 1, ans);
            return ans;
        }

        static long CalculateCount(string[] stones, int iterations) => stones.Sum(stone => IterateAndCount(stone, iterations));
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string[] stones = lines[0].Split(' ').ToArray();
            
            long total = CalculateCount(stones, 100);
            
            Console.WriteLine(total);
        }
    }
}