using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Testbed
{
    internal class Testbed
    {
        static bool TrueEquation(string equation)
        {
            long target = long.Parse(equation.Split('=')[0]);
            equation = equation.Split('=')[1].Replace(" ", "").Replace("(", "").Replace(")", "") + "\n";

            int index = equation.IndexOfAny(new char[] { '+', '*' });
            long num1 = long.Parse(equation.Substring(0, index));
            char operation = equation[index];  
            equation = equation.Substring(index + 1);
            
            long num2 = 0;
            int i = -1;
            long result = 0;
            
            while (i < equation.Length - 1)
            {
                i++;
                if (char.IsDigit(equation[i])) continue;
                if (equation[i] != '+' && equation[i] != '*' && equation[i] != '\n') continue;
                
                num2 = long.Parse(equation.Substring(0, i));
                result = operation == '*' ? num1 * num2 : num1 + num2;

                num1 = result;
                operation = equation[i];
                equation = equation.Substring(i + 1);
                i = -1;
            }
            
            return result == target;
        }
        
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            foreach (string line in lines)
            {
                Console.WriteLine(line.PadRight(40) + " " + TrueEquation(line));
            }
        }
    }
}