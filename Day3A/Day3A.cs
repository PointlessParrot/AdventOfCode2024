

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day3A
{
    internal class Day3A
    {

        static int Multiply(string input)
        { 
            Console.WriteLine(input);
            string[] str = input.Split(')')[0].Split(',');
            if (str.Length != 2) return 0;
            if (!int.TryParse(str[0], out int int1) || str[0].Contains(' ')) return 0;
            if (!int.TryParse(str[1], out int int2) || str[1].Contains(' ')) return 0;
            Console.WriteLine(int1.ToString() + ',' + int2.ToString() + "                                       :RETURNED");
            return int1 * int2;
        }
        
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText("input.txt");
            //text = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
            int result = text.Split(new[] { "mul(" }, StringSplitOptions.None).Select(x => Multiply(x)).Sum();
            Console.WriteLine(result);
        }
    }
}