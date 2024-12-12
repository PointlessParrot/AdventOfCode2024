// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day12A
{
    internal class Day12A
    {
        static void ExpandRegion(char[,] grid, int c0, int c1, List<(int, int)> coordinates, char type)
        {
            foreach ((int a0, int a1) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1) })
            {
                (int, int) coordinate = (c0 + a0, c1 + a1);
                try
                {
                    if (coordinates.Contains(coordinate)) continue;
                    if (grid[coordinate.Item1, coordinate.Item2] != type) continue;
                    
                    coordinates.Add(coordinate);
                    ExpandRegion(grid, coordinate.Item1, coordinate.Item2, coordinates, type);
                }
                catch (IndexOutOfRangeException)
                {
                    //Ignore
                }
            }
        }

        static (int, int)[][] GetRegions(char[,] grid)
        {
            List<(int, int)[]> regions = new List<(int, int)[]>();
            bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            
            for (int index0 = 0; index0 < grid.GetLength(0); index0++)
            for (int index1 = 0; index1 < grid.GetLength(1); index1++)
            {
                if (visited[index0, index1]) continue;
                
                List<(int, int)> coordinates = new List<(int, int)>() { (index0, index1) };
                char type = grid[index0, index1];
                ExpandRegion(grid, index0, index1, coordinates, type);
                
                regions.Add(coordinates.ToArray());
                
                foreach (var coordinate in coordinates)
                    visited[coordinate.Item1, coordinate.Item2] = true;
            }
            
            return regions.ToArray();
        }

        static int GetPerimeter((int, int)[] region)
        {
            int perimeter = 0;
            
            foreach ((int c0, int c1) in region)
            foreach ((int a0, int a1) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1) })
            {
                (int, int) coordinate = (c0 + a0, c1 + a1);
                try
                {
                    if (region.Contains(coordinate)) continue;
                    perimeter++;
                }
                catch (IndexOutOfRangeException)
                {
                    //Ignore
                }
            }
            
            return perimeter;
        }

        static int GetPrice((int, int)[] region) => GetPerimeter(region) * region.Length;

        static char[,] ToGrid(string[] input)
        {
            char[,] grid = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[0].Length; j++)
                grid[i, j] = input[i][j];
            
            return grid;
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[,] grid = ToGrid(lines);
            (int, int)[][] regions = GetRegions(grid);
            int price = regions.Select(GetPrice).Sum();
            Console.WriteLine(price);
        }
    }
}