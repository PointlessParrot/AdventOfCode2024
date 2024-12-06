

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day6A
{
    internal class Day6A
    {
        static bool? CheckMove(char[,] grid, (int, int) location, (int, int) direction)
        {
            try
            {
                char c = grid[location.Item1 + direction.Item1, location.Item2 + direction.Item2];
                return (c != '#');
            }
            catch (Exception)
            {
                return null;
            }
        }

        static void TurnRight(ref (int, int) direction) =>
            direction = direction.Item1 == 0 ? (direction.Item2, 0) : (0, -direction.Item1);

        static bool TryMoveForwards(char[,] grid, ref (int, int) location, (int, int) direction, int[,] visited)
        {
            grid[location.Item1, location.Item2] = '.';
            visited[location.Item1, location.Item2] = 1;
            location = (location.Item1 + direction.Item1, location.Item2 + direction.Item2);
            try
            {
                grid[location.Item1, location.Item2] = 'P';
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        static int[,] FindPath(char[,] grid, (int, int) location, (int, int) direction)
        {
            int[,] visited = new int[grid.GetLength(0), grid.GetLength(1)];

            while (true)
            {
                while (true)
                {
                    //WriteGrid(grid);
                    bool result = CheckMove(grid, location, direction) ?? true;
                    if (result) break;
                    TurnRight(ref direction);
                }

                if (!TryMoveForwards(grid, ref location, direction, visited)) break;
            }

            return visited;
        }

        static char[,] GetGrid(string[] lines)
        {
            char[,] grid = new char[lines[0].Length, lines.Length];
            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    grid[i, j] = lines[i][j];

            return grid;
        }

        static bool TryFindElement(char[,] grid, char target, ref (int, int) location)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    if (grid[i, j] == target)
                    {
                        location = (i, j);
                        return true;
                    }

            location = (-1, -1);
            return false;
        }

        static int SumGrid(int[,] grid)
        {
            int total = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                total += grid[i, j];

            return total;
        }

        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[,] grid = GetGrid(lines);
            (int, int) location = (-1, -1);
            (int, int) direction = (-1, 0);
            foreach (char target in new[] { '^', '>', 'v', '<' })
                if (TryFindElement(grid, target, ref location))
                {
                    int value = Array.IndexOf(new[] { '^', '>', 'v', '<' }, target);
                    for (int i = 0; i < value; i++) TurnRight(ref direction);
                    break;
                }

            int[,] visited = FindPath(grid, location, direction);
            int total = SumGrid(visited);

            Console.WriteLine(total);
        }

        static void WriteGrid<T>(T[,] grid)
        {
            Console.Clear();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }



    }
}