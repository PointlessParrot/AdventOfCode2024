// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day14A
{
    internal class Day14A
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int height = 103;
            int width = 101;

            ((int, int), (int, int))[] robots;
            robots = lines.Select(line => line.Split(' ')
                    .Select(pair => pair.Substring(2).Split(',')
                        .Select(int.Parse))
                    .Select(pair => (pair.ElementAt(1), pair.ElementAt(0))))
                .Select(quartet => (quartet.ElementAt(0), quartet.ElementAt(1)))
                .ToArray();

            for (int i = 0; i < robots.Length; i++)
            {
                robots[i] = (
                    (robots[i].Item1.Item1 + robots[i].Item2.Item1 * 100,
                        robots[i].Item1.Item2 + robots[i].Item2.Item2 * 100),
                    robots[i].Item2);
                robots[i] = (
                    ((robots[i].Item1.Item1 % height + height) % height,
                        (robots[i].Item1.Item2 % width + width) % width), 
                    robots[i].Item2);
            }

            ((int, int), (int, int)) quadrants = ((0, 0), (0, 0));

            foreach (var robot in robots)
            {
                if (robot.Item1.Item1 < (height - 1) / 2)
                {
                    if (robot.Item1.Item2 < (width - 1) / 2)                    
                        quadrants.Item1.Item1++;
                    
                    else if (robot.Item1.Item2 > (width - 1) / 2)                    
                        quadrants.Item1.Item2++;
                }

                if (robot.Item1.Item1 > (height - 1) / 2)
                {
                    if (robot.Item1.Item2 < (width - 1) / 2)
                        quadrants.Item2.Item1++;

                    if (robot.Item1.Item2 > (width - 1) / 2)
                        quadrants.Item2.Item2++;
                }
            }
            
            int total = quadrants.Item1.Item1 * quadrants.Item1.Item2 * quadrants.Item2.Item1 * quadrants.Item2.Item2;
            Console.WriteLine(total);

            int[,] grid = new int[height, width];
            foreach (var robot in robots)
                grid[robot.Item1.Item1, robot.Item1.Item2]++;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j] == 0 ? "." : grid[i, j].ToString());
                }
                Console.WriteLine();
            }
        }
    }
}