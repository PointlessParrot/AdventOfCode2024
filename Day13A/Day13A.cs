// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day13A
{
    internal class Day13A
    {
        //16a + 53b = 6788
        //32a + 11b = 4716

        static int[,] Gridify(string[] strings)
        {
            strings = strings.Select(line => line.Split(':')[1]).ToArray();
            string[][] input = strings.Select(line => line.Split(',').Select(s => s.Trim().Replace("-", "+-").Substring(2)).ToArray()).ToArray();
            int[,] grid = new int[2, 3];
            for (int i = 0; i < 2; i++)
            for (int j = 0; j < 3; j++)
                grid[i, j] = int.Parse(input[j][i]);
            
            return grid;
        }

        static void SolveGrid(int[,] grid)
        {
            int a1 = grid[0, 0];
            int a2 = grid[1, 0];

            for (int i = 0; i < 3; i++)
            {
                grid[0, i] *= a2;
                grid[1, i] *= a1;
                grid[0, i] -= grid[1, i];
            }
        }

        static int GetAnswers(int[,] grid)
        {
            int b = grid[0, 1];
            if (grid[0, 2] % b != 0)
                return 0;

            int valueB = grid[0, 2] / b;
            
            int b1 = grid[1, 1];
            grid[1, 1] = 0;
            grid[1, 2] -= b1 * valueB;
            
            int a = grid[1, 0];
            if (grid[1, 2] % a != 0)
                return 0;
            
            int valueA = grid[1, 2] / a;
            
            return 3*valueA + valueB;
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int total = 0;
            for (int k = 0; k < lines.Length - 2; k += 4)
            {
                int[,] grid = Gridify(lines.Skip(k).Take(3).ToArray());
                SolveGrid(grid);
                total += GetAnswers(grid);
            }
            Console.WriteLine(total);

            Console.ReadKey();
        }
    }
}