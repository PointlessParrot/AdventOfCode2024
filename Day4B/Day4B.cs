// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day4B
{
    internal class Day4B
    {
        static int Search(ref string[] grid, int x, int y)
        {
            string word = "MAS";
            int total = 0;
            (int, int)[] directions = new[] { (1, 1), (-1, 1), (-1, -1), (1, -1) };
            
            for (int a = 0; a < directions.Length; a++)
            {
                for (int b = a + 1; b < directions.Length; b++)
                {
                    bool flag = true;
                    for (int j = -1; j < word.Length - 1; j++)
                    {
                        try
                        {
                            if (word[j + 1] == grid[y + j * directions[a].Item2][x + j * directions[a].Item1] &&
                                word[j + 1] == grid[y + j * directions[b].Item2][x + j * directions[b].Item1]) continue;
                        }
                        catch (Exception e)
                        {
                            // ignored
                        }

                        flag = false;
                        break;
                    }

                    if (flag)
                    {
                        total++;
                        break;
                    }
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