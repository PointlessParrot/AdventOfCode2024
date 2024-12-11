// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day11A
{
    internal class Day11A
    {
        static bool StoneCheck(List<long> stones, int index )
        {
            long stone = stones[index];
            if (stone == 0)
            {
                stones[index] = 1;
                return false;
            }
            if (stone.ToString().Length % 2 == 0)
            {
                int len = stone.ToString().Length / 2;
                stones[index] = long.Parse(stone.ToString().Substring(len));
                stones.Insert(index, long.Parse(stone.ToString().Substring(0, len)));
                return true;
            } 
            stones[index] = stone * 2024;
            return false;
        }
        
        static void Blink(List<long> stones)
        {
            for (int i = 0; i < stones.Count; i++)
            {
                if (StoneCheck(stones, i)) i++;
            }
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<long> stones = lines[0].Split(' ').Select(long.Parse).ToList();
            int iterations = 25;
            
            for (int i = 0; i < iterations; i++)
                Blink(stones);
            
            Console.WriteLine(stones.Count);
        }
    }
}