

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day9A
{
    internal class Day9A
    {
        static int?[] IntoArray(string inputLine)
        {
            int len = inputLine.Select<char, int>(numChar => int.Parse(numChar.ToString())).Sum();
            int[] numLine = inputLine.Select(c => int.Parse(c.ToString())).ToArray();
            int?[] arr = new int?[len];
            
            int index = 0;
            for (int i = 0; i < numLine.Length; i++)
            {
                for (int j = 0; j < numLine[i]; j++)
                {
                    arr[index] = i % 2 == 0 ? i / 2 : null;
                    index++;
                }
            }
            
            return arr;
        }

        static int LastIndexNotNull(int?[] inputArray)
        {
            for (int i = inputArray.Length - 1; i > -1; i--)
            {
                if (inputArray[i] != null) return i;
            }

            return -1;
        }
        
        static int?[] Compact(int?[] inputArray)
        {
            int index1 = Array.IndexOf(inputArray, null);;
            int index2 = LastIndexNotNull(inputArray);
            while (index1 < index2)
            {
                inputArray[index1] = inputArray[index2];
                inputArray[index2] = null;
                index1 = Array.IndexOf(inputArray, null);
                index2 = LastIndexNotNull(inputArray);
            }
            
            return inputArray;
        }

        static long Checksum(int?[] inputArray) => inputArray.Select((value, position) => (value ?? 0) * (long)position).Sum();
        
        static void Main(string[] args)
        {
            string line = System.IO.File.ReadAllLines("input.txt")[0];
            
            int?[] inArray = IntoArray(line);
            int?[] outArray = Compact(inArray);
            long checksum = Checksum(outArray);
            Console.WriteLine(checksum);
        }
    }
}