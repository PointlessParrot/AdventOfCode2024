// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Day9B
{
    internal class Day9B
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

        static void FindBlocks(int?[] inputArray, out (int, int)[] blocks, out (int, int)[] gaps)
        {
            List<int> gapSwap = new List<int>();
            List<int> blockSwap = new List<int>();

            blockSwap.Add(0);
            bool nullArea = false;
            int? lastNum = inputArray[0];
            for (int i = 1; i < inputArray.Length; i++)
            {
                if (inputArray[i] == lastNum) continue;
                
                if (lastNum == null || inputArray[i] == null)
                {
                    nullArea = !nullArea;
                    lastNum = inputArray[i];
                    blockSwap.Add(i);
                    gapSwap.Add(i); 
                }
                else
                {
                    lastNum = inputArray[i];
                    blockSwap.Add(i);
                    blockSwap.Add(i);
                }
            }

            if (nullArea) gapSwap.Add(inputArray.Length);
            else blockSwap.Add(inputArray.Length);
            
            gaps = new (int, int)[gapSwap.Count / 2];
            blocks = new (int, int)[blockSwap.Count / 2];
            
            for (int i = 0; i < gapSwap.Count; i += 2)
            {
                gaps[i / 2] = (gapSwap[i], gapSwap[i + 1] - gapSwap[i]);
            }

            for (int i = 0; i < blockSwap.Count; i += 2)
            {
                blocks[i / 2] = (blockSwap[i], blockSwap[i + 1] - blockSwap[i]);
            }
        }
        
        static int?[] ContiguousCompact(int?[] inputArray)
        {
            (int, int)[] blocks;
            (int, int)[] gaps;
            FindBlocks(inputArray, out blocks, out _);
            blocks = blocks.Reverse().ToArray();
            foreach ((int pos, int len) block in blocks)
            {
                FindBlocks(inputArray, out _, out gaps);
                foreach ((int pos, int len) gap in gaps)
                {
                    if (gap.pos > block.pos) break;
                    if (block.len > gap.len) continue;
                    
                    int? num = inputArray[block.pos];
                    for (int k = 0; k < block.len; k++)
                    {
                        inputArray[block.pos + k] = null;
                        inputArray[gap.pos + k] = num;
                    }
                    break;
                }
            }
            
            
            return inputArray;
        }

        static long Checksum(int?[] inputArray) => inputArray.Select((value, position) => (value ?? 0) * (long)position).Sum();
        
        static void Main(string[] args)
        {
            string line = System.IO.File.ReadAllLines("input.txt")[0];
            
            int?[] inArray = IntoArray(line);
            int?[] outArray = ContiguousCompact(inArray);
            long checksum = Checksum(outArray);
            Console.WriteLine(checksum);
        }
    }
}