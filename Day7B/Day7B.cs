// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace Day7B
{
    internal class Day7B
    {
        static long Evaluate(string expression)
        {
            expression += '\n';

            int index = 0;
            long num1 = 0;
            long num2 = 0;
            char operation = ' ';
            int i = -1;
            long result = 0;

            index = expression.IndexOfAny(new char[] { '~', '+', '*' });
            num1 = long.Parse(expression.Substring(0, index));
            operation = expression[index];  
            expression = expression.Substring(index + 1);
            
            while (i < expression.Length - 1)
            {
                i++;
                if (char.IsDigit(expression[i])) continue;
                
                num2 = long.Parse(expression.Substring(0, i));
                result = operation switch
                {
                    '~' => long.Parse($"{num1}{num2}"),
                    '+' => num1 + num2,
                    '*' => num1 * num2
                };

                num1 = result;
                operation = expression[i];
                expression = expression.Substring(i + 1);
                i = -1;
            }
            
            return result;
        }
        
        static bool TryFindNextExpression(ref string expression)
        {
            if (expression.IndexOf('~') == -1 && expression.IndexOf('+') == -1) return false;
            int index1 = expression.LastIndexOf('~');
            int index2 = expression.LastIndexOf('+');
            int index = index1 == -1 ? index2 : index2 == -1 ? index1 : index1 > index2 ? index1 : index2;
            char op = expression[index] == '~' ? '+' : '*';
            expression = expression.Substring(0, index) + op + expression.Substring(index + 1).Replace('*', '~');
            
            return true;
        }

        static bool TryParseEquation(string input, out long target, out string expression)
        {
            target = 0; expression = string.Empty;
            if (input == string.Empty)
                return false;
            
            int index = input.IndexOf(':');
            target = long.Parse(input.Substring(0, index));
            expression = input.Substring(index + 1).Trim().Replace(" ", "~");
            return true;
        }

        static long EquationIsPossible(string input)
        {
            if (!TryParseEquation(input, out long target, out string expression)) return 0;

            while (true)
            {
                long result = Evaluate(expression);
                if (result == target) return target;
                if (!TryFindNextExpression(ref expression)) return 0;
            }
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            long[] results = lines.Select(EquationIsPossible).ToArray();
            long total = results.Sum();
            Console.WriteLine(total);
        }
    }
}