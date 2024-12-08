// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace Day7A
{
    internal class Day7A
    {

        static string ToBinary(int number)
        {
            string binaryString = string.Empty;
            int value = (int)Math.Log2(number) + 1;
            for (int i = 0; i < value; i++)
            {
                binaryString = number % 2 + binaryString;
                number /= 2;
            }   
            return binaryString;
        }

        static long PlusOrTimes(long a, long b, char operation) => operation == '0' ? a + b : a * b;

        static long RunEquation(int[] values, string binary)
        {
            long[] numbers = values.Select(x => (long)x).ToArray();
            for (int i = 1; i < values.Length; i++)
            {
                numbers[i] = PlusOrTimes(numbers[i - 1], numbers[i], binary[i - 1]);
            }
            return numbers[numbers.Length - 1];
        }
        
        static long EquationValue(string equation)
        {
            //Convert to int + int[]
            string[] equationNew = equation.Split(':');
            long target = long.Parse(equationNew[0]);
            int[] values = equationNew[1].Trim().Split(' ').Select(int.Parse).ToArray();
            
            //Loops
            for (int i = 0; i < Math.Pow(2, values.Length - 1); i++)
            {
                string index = ToBinary(i).PadLeft(values.Length - 1, '0');
                if (RunEquation(values, index) == target)
                {
                    string[] v1 = values.Select((num, i) => ((' ' + index)[i] == ' ' ? "" : (' ' + index)[i] == '0' ? " + " : " * ")  + num ).ToArray();
                    Console.WriteLine($"{target} = {new string('(', index.Length) + string.Join(")", v1)}");
                    
                    return target;
                }
            }
            
            return 0;
        }

        static void Tester(string equation)
        {
            string[] equationNew = equation.Split(':');
            long target = long.Parse(equationNew[0]);
            int[] values = equationNew[1].Trim().Split(' ').Select(int.Parse).ToArray();

            Console.WriteLine();
            for (int i = 0; i < Math.Pow(2, values.Length - 1); i++)
            {
                string index = ToBinary(i).PadLeft(values.Length - 1, '0');
                string[] v1 = values.Select((num, i) => ((' ' + index)[i] == ' ' ? "" : (' ' + index)[i] == '0' ? " + " : " * ")  + num ).ToArray();
                Console.WriteLine($"{target} = {new string('(', index.Length) + string.Join(")", v1)}");
            }
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            long[] result = lines.Select(EquationValue).ToArray();
            long ans = result.Sum();
            Console.WriteLine(ans);
            
            Tester(lines[0]);
        }
    }
}