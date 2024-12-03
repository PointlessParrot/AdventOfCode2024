// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day3B
{
    internal class Day3B
    {

        static int Multiply(string input)
        { 
            string[] str = input.Split(')')[0].Split(',');
            if (str.Length != 2) return 0;
            if (!int.TryParse(str[0], out int int1) || str[0].Contains(' ')) return 0;
            if (!int.TryParse(str[1], out int int2) || str[1].Contains(' ')) return 0;
            return int1 * int2;
        }

        static List<int>  Find(string input, string target)
        {
            int offfset = 0;
            List<int> output = new List<int>();
            while (input.Contains(target))
            {
                int index = input.IndexOf(target);
                output.Add(index + offfset);
                input = input.Substring(index + target.Length);
                offfset += index + target.Length;
            }

            return output;
        }
        
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText("input.txt");
            //text = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
            int total = 0;
            bool enabled = true;
            
            List<int> mulIndex = Find(text, "mul(");
            List<int> doIndex = Find(text, "do()");
            List<int> dontIndex = Find(text, "don't()");
            
            while (mulIndex.Count > 0 && dontIndex.Count > 0 && doIndex.Count > 0)
            {
                if (mulIndex[0] < dontIndex[0] && mulIndex[0] < doIndex[0])
                {
                    if(enabled) total += Multiply(text.Substring(mulIndex[0] + 4, text.Length - mulIndex[0] - 4));
                    mulIndex.RemoveAt(0);
                }

                else if (doIndex[0] < dontIndex[0])
                {
                    enabled = true;
                    doIndex.RemoveAt(0);
                }

                else
                {
                    enabled = false;
                    dontIndex.RemoveAt(0);
                }
            }
            if (mulIndex.Count == 0) {}
            else if (dontIndex.Count == 0)
            {
                if (!enabled)
                {
                    while (true)
                    {
                        if (mulIndex.Count() == 0) break;
                        if (mulIndex[0] > doIndex[0]) break;
                        mulIndex.RemoveAt(0);
                    }
                }
                while (true)
                {
                    if (mulIndex.Count == 0) break;
                    total += Multiply(text.Substring(mulIndex[0] + 4, text.Length - mulIndex[0] - 4));
                    mulIndex.RemoveAt(0);
                }
            }
            else
            {
                if (enabled)
                {
                    while (true)
                    {
                        if (mulIndex.Count == 0) break;
                        if (mulIndex[0] > dontIndex[0]) break;
                        total += Multiply(text.Substring(mulIndex[0] + 4, text.Length - mulIndex[0] - 4));
                        mulIndex.RemoveAt(0);
                    }
                }
            }
            
            Console.WriteLine(total);
        }
    }
}