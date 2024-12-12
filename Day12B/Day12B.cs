// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day12B
{
    internal class Day12B
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

        static bool AreAdjacent((int, int) coordinate1, (int, int) coordinate2)
            => Math.Abs(coordinate1.Item1 - coordinate2.Item1) + Math.Abs(coordinate1.Item2 - coordinate2.Item2) == 1;

        static int GetSides((int, int)[] region)
        {
            int sides = 0;
            List<(int, int)>[] edges = new List<(int, int)>[4];
            (int, int)[] directions = new[] { (1, 0), (0, 1), (-1, 0), (0, -1) };
            
            for (int index = 0; index < edges.Length; index++)
                edges[index] = new List<(int, int)>();
            
            foreach ((int c0, int c1) in region) 
                for (int index = 0; index < directions.Length; index++)
                {
                    (int a0, int a1) = directions[index];
                    (int, int) coordinate = (c0 + a0, c1 + a1);
                    if (region.Contains(coordinate)) continue;
                    edges[index].Add(coordinate);
                }

            foreach (var edgeList in edges)
            {
                sides += edgeList.Count;
                for (int i = 0; i < edgeList.Count; i++)
                for (int j = i + 1; j < edgeList.Count; j++)
                {
                    if (AreAdjacent(edgeList[i], edgeList[j])) sides--;
                }
            }

            return sides;
        }

        static int GetPrice((int, int)[] region) => GetSides(region) * region.Length;

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

            foreach (var region in regions)
            {
                Console.WriteLine(GetPrice(region));
            }
        }
    }
}