// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day14B
{
    internal class Day14B
    {
        static void StepRobots(ref ((int, int), (int, int))[] robots, int height, int width)
        {
            for (int i = 0; i < robots.Length; i++)
            {
                robots[i] = (
                    (robots[i].Item1.Item1 + robots[i].Item2.Item1,
                        robots[i].Item1.Item2 + robots[i].Item2.Item2),
                    robots[i].Item2);
                
                robots[i] = (
                    ((robots[i].Item1.Item1 % height + height) % height,
                        (robots[i].Item1.Item2 % width + width) % width), 
                    robots[i].Item2);
            }
        }

        static ((int, int), (int, int)) FindQuadrants(((int, int), (int, int))[] robots, int height, int width)
        {
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
            
            return quadrants;
        }

        static int[,] MakeGrid(((int, int), (int, int))[] robots, int height, int width)
        {
            int[,] grid = new int[height, width];
            foreach (var robot in robots)
                grid[robot.Item1.Item1, robot.Item1.Item2]++;
            
            return grid;
        }

        static void PrintRobots(((int, int), (int, int))[] robots, int height, int width)
        {
            int[,] grid = MakeGrid(robots, height, width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j] == 0 ? "." : grid[i, j].ToString());
                }
                Console.WriteLine();
            }
        }

        static bool VerticalCheck(((int,int),(int,int))[] robots, int height, int width)
        {
            int[,] grid = MakeGrid(robots, height, width);
            const int checkLength = 10;
            
            for (int j = 0; j < width; j++)
            {
                for (int i = 0; i < height - checkLength + 1; i++)
                {
                    bool check = true;
                    for (int k = 0; k < checkLength; k++)
                    {
                        if (grid[i + k, j] == 0)
                        {
                            check = false;
                        }
                    }
                    if (check)
                        return true;
                }
            }
            return false;
        }
        
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

            for (int i = 0; i < 7892; i++)
                StepRobots(ref robots, height, width);
            
            for (int i = 7892; i < int.MaxValue; i += 10403)
            {    
                PrintRobots(robots, height, width);
                Console.WriteLine(i);
                Console.WriteLine();

                for (int j = 0; j < 10403; j ++)
                    StepRobots(ref robots, height, width);
            }
        }
    }
}