

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day8A
{
    internal class Day8A
    {
        static void AddNodes((int, int) antenna1, (int, int) antenna2, int[,] nodes)
        {
            int diff1 = antenna1.Item1 - antenna2.Item1;
            int diff2 = antenna1.Item2 - antenna2.Item2;
            
            try { nodes[antenna1.Item1 + diff1, antenna1.Item2 + diff2] = 1; }
            catch (IndexOutOfRangeException) {} //Ignore 

            try { nodes[antenna2.Item1 - diff1, antenna2.Item2 - diff2] = 1; }
            catch (IndexOutOfRangeException) {} //Ignore
        }

        static List<(List<(int, int)>, char)> GetAllLocations(char[,] grid)
        {
            List<(List<(int, int)>, char)> locations = new List<(List<(int, int)>, char)>();
            
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '.') continue;
                    
                    char target = grid[i, j];
                    bool found = false;
                    foreach (var element in locations)
                    {
                        if (element.Item2 != target) continue;
                        
                        element.Item1.Add((i, j));
                        found = true;
                        break;
                    }
                    if (found) continue;
                    
                    locations.Add((new List<(int, int)>(), target));
                    locations[locations.Count - 1].Item1.Add((i, j));
                }

            return locations;
        }

        static (int, int)[] GetCombinationPairs(int numOfItems)
        {
            List<(int, int)> pairs = new List<(int, int)>();
            for (int i = 0; i < numOfItems; i++)
                for (int j = i + 1; j < numOfItems; j++)
                    pairs.Add((i, j));
            
            return pairs.ToArray();
        }

        static int[,] FindNodeGrid(char[,] grid)
        {
            int[,] nodes = new int[grid.GetLength(0), grid.GetLength(1)];
            List<(List<(int, int)>, char)> locations = GetAllLocations(grid);

            foreach ((List<(int, int)>, char) element in locations )
            {
                (int, int)[] combinationPairs = GetCombinationPairs(element.Item1.Count);
                foreach ((int index1, int index2) in combinationPairs)
                {
                    AddNodes(element.Item1[index1], element.Item1[index2], nodes);
                }
            }

            return nodes;
        }

        static int Sum(int[,] grid)
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
            char[,] data = new char[lines.Length, lines[0].Length];
            
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    data[i, j] = lines[i][j];
            
            int[,] nodes = FindNodeGrid(data);
            int total = Sum(nodes);
            Console.WriteLine(total);
        }
    }
}