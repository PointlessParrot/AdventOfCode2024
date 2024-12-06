// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;

namespace AOC_Utilities
{
    public class Grid<T>
    {
        public int Width;
        public int Height;
        protected T[,] _data;

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _data = new T[width, height];
        }
    }

    public class CharGrid : Grid<char>
    {
        public CharGrid(int width, int height) : base(width, height)
        {
            Width = width;
            Height = height;
            _data = new char[width, height];
        }
    }
    
    internal class Utilities
    {
        static void Main(string[] args)
        {
            Grid<int> grid = new Grid<int>(4, 4);
        }
    }
}