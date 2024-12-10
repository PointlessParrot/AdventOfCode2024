

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day10A
{
    internal class Day10A
    {
        static (int, int)[] FindTrailHeads(char[,] grid)
        {
            List<(int, int)> result = new List<(int, int)>();

            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    if (grid[i, j] == '0') result.Add((i, j));
            
            return result.ToArray();
        }

        static bool NextMoves(char[,] grid, (int, int) location, out List<(int, int)> moves)
        {
            List<(int, int)> result = new List<(int, int)>();
            
            (int y, int x) = location;
            int a = 1;
            int b = 0;
            char c = grid[location.Item1, location.Item2];
            c = (char)(c + 1);
            
            for (int i = 0; i < 2; i++)
            {    
                for (int j = 0; j < 2; j++)
                {
                    a *= -1;
                    b *= -1;
                    if (y + a > grid.GetLength(0) - 1 || y + a < 0 || x + b > grid.GetLength(1) - 1 || x + b < 0) continue;
                    if (grid[y + a, x + b] == c) result.Add((y + a, x + b));
                }
                b = a;
                a = 0;
            }
            
            moves = result;
            return c == '9';
        }
        
        static List<(int, int)> FindTrailEnds(char[,] grid, (int, int) location)
        {
            if (NextMoves(grid, location, out List<(int, int)> moves)) return moves;

            List<(int, int)> movesNew = new List<(int, int)>();
            foreach ((int, int) xy in moves)
            { 
                movesNew = movesNew.Union(FindTrailEnds(grid, xy)).ToList();
            }
            return movesNew;
        }

        static int FindTrailHeadScore(char[,] grid, (int, int) location)
        {
            List<(int, int)> moves = FindTrailEnds(grid, location);
            return moves.Count;
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[,] data = new char[lines.Length, lines[0].Length];
            
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    data[i, j] = lines[i][j];
            
            (int, int)[] trailHeads = FindTrailHeads(data);
            int[] totals = trailHeads.Select(xy => FindTrailHeadScore(data, xy)).ToArray();
            int sum = totals.Sum();
            Console.WriteLine(sum);
        }
    }
}