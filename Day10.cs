using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
         
    public class Day10
    {

        private string inputFile;
        private int joltDifference;

        private string[] inputText;

        private int[] adapterList;

        public Day10(string anInputFile, int aJoltDifference)
        {
            Console.WriteLine("Starting Advent Of Code Day 10");

            this.inputFile = anInputFile;
            this.joltDifference = aJoltDifference;

            this.Initilize();
            RunDay10Part1();

            this.Initilize();
            RunDay10Part2(this.joltDifference);
        }

        private void Initilize()
        {
            this.inputText = System.IO.File.ReadAllText(this.inputFile).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            this.adapterList = new int[this.inputText.Length];

            for(int inputTextIndex = 0; inputTextIndex < this.adapterList.Length; inputTextIndex++)
            {
                adapterList[inputTextIndex] = Int32.Parse(inputText[inputTextIndex]);
            }

            Array.Sort(this.adapterList);
        }

        private void RunDay10Part1()
        {
            int countDifference1 = 0;
            int countDifference2 = 0;
            int countDifference3 = 0;

            // Initially, 0 is the starting jolt for the outlet
            int previousNumber = 0;

            for (int index = 0; index < this.adapterList.Length; index++)
            {
                switch (this.adapterList[index] - previousNumber)
                {
                    case 1:
                        countDifference1++;
                        break;
                    case 2:
                        countDifference2++;
                        break;
                    case 3:
                        countDifference3++;
                        break;
                    default:
                        Console.WriteLine("Voltage difference error found");
                        break;
                }

                previousNumber = this.adapterList[index];
            }

            // +3 for device adapter
            countDifference3++;

            int problemAnswer = countDifference1 * countDifference3;

            Console.WriteLine("Day10Part1 Answer : " + problemAnswer);

        }
        private void RunDay10Part2(int aJoltDifference)
        {
            UInt64 permutationCount = CountPermutations(aJoltDifference);

            Console.WriteLine("Day10Part2 Answer : " + permutationCount);

        }

        private UInt64 CountPermutations(int aJoltDifference)
        {
            UInt64[] countList = new UInt64[adapterList.Length];

            for(int index1 = adapterList.Length; index1 > 0; index1--)
            {
                // We are at the top of the list, set the value to 1
                if(index1 == adapterList.Length)
                {
                    countList[index1 - 1] = 1;
                }
                else
                {
                    for(int index2 = index1; index2 < countList.Length; index2++)
                    {
                        if(adapterList[index2] - adapterList[index1-1] > aJoltDifference)
                        {
                            continue;
                        }
                        else
                        {
                            countList[index1 - 1] += countList[index2];
                        }
                    }
                }
            }

            UInt64 permutationCount = 0;

            // Add in the power source
            for(int index = 0; index < countList.Length; index++)
            {
                if(adapterList[index] <= aJoltDifference )
                {
                    permutationCount += countList[index];
                }
            }

            return permutationCount;
        }

    }
}
