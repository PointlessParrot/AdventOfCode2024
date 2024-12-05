

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day4A
{
    internal class Day4A
    {
        static int Search(ref string[] grid, int x, int y)
        {
            string word = "XMAS";
            int total = 0;
            (int, int)[] directions = new[] { (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1) };
            
            foreach ((int, int) direction in directions)
            {
                total++;
                for (int j = 0; j < word.Length; j++)
                {
                    try
                    {
                        if (word[j] == grid[y + j * direction.Item2][x + j * direction.Item1]) continue;
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                    total--;
                    break;
                }
            }
            
            return total;
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int total = 0;
            for (int i = 0; i < lines.Length; i++) 
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                        total += Search(ref lines, j, i);
                }
            }
            Console.WriteLine(total);
        }
    }
}