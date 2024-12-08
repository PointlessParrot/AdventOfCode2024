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

        public Grid(T[,] data)
        {
            Height = data.GetLength(0);
            Width = data.GetLength(1);
            Data = data;
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
        
    }
    
    internal class Utilities
    {
        public struct Binary()
        {
            string _binaryData;

            public Binary(string binaryString) : this()
            {
                _binaryData = binaryString;
            }

            public static implicit operator Binary(int number)
            {
                string binaryString = string.Empty;
                int value = (int)Math.Log2(number) + 1;
                for (int i = 0; i < value; i++)
                {
                    binaryString = number % 2 + binaryString;
                    number /= 2;
                }   
                return new Binary(binaryString);
            }

            public static explicit operator int(Binary binary)
            {
                int length = binary._binaryData.Length;
                return binary._binaryData.Select((x, i) => (int)x * (int)Math.Pow(2, length - i)).Sum();
            }
            
            public static explicit operator string(Binary binary) => binary._binaryData;

            public static Binary operator ++(Binary binary)
            {
                int length = binary._binaryData.Length;
                int index = binary._binaryData.LastIndexOf('0');
                string binaryString = binary._binaryData.Substring(0, index) + '1' + new String('0', length - index - 1);
                return new Binary(binaryString);
            }
        }
        static void Main(string[] args)
        {
            Grid<int> grid = new Grid<int>(4, 4);
        }
    }
}