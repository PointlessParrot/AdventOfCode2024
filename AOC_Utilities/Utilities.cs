// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
// ReSharper disable ConvertToPrimaryConstructor

namespace AOC_Utilities
{
    public class Grid<T>
    {
        public int Height;
        public int Width;
        protected T[,] Data;

        public Grid(int height, int width)
        {
            Height = height;
            Width = width;
            Data = new T[height, width];
        }
        public Grid(T[,] input)
        {
            Height = input.GetLength(0);
            Width = input.GetLength(1);
            Data = input;
        }

        public T this[int a, int b]
        {
            get => Data[a, b];
            set => Data[a, b] = value;
        }

        public Grid<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(this);
            
            Grid<TResult> result = new Grid<TResult>(Height, Width);
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    result[i, j] = selector(Data[i, j]);
            return result;
        }
    }

    public class CharGrid : Grid<char>
    {
        public CharGrid(int height, int width) : base(height, width) {}
        public CharGrid(char[,] data) : base(data) {}
        public CharGrid(string[] lines) : base(1, 1)
        {
            Data = new char[lines.Length,lines.Select(x => x.Length).Max()];
            Width = Data.
            lines.Select(x => x.PadRight(Data.GetLength(1)));
            for (int i = 0; i < Data.GetLength(0); i++)
                for (int j = 0; j < Data.GetLength(1); j++)
                    Data[i, j] = lines[i][j];
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