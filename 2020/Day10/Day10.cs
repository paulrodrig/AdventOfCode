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
            // I can clean this up if needed, but here's the premise of the code:
            //
            // To get to the device, there is only one way to do this, you need to go through
            // the largest number. This is going to be true for all permutations.
            //
            // Then, consider the second last number. How many paths take you to the highest
            // adapter (considering the jolt difference)? Rinse and repeat until you go through
            // all the adapters and outlet.
            
            // Connections look like : outlet - adapter - adapter - [... adapter ...] - device
                 
            // Example
            // index         0   1   2   3   4   5    6    7    8
            // adapterList:  1   2   3   6   7   9   10   12   14

            // countList  :                                     1 <- only one way for 14 to get to the device [17]
            // countList  :                                1    1 <- only one way for 12 to get to the next number [14]
            // countList  :                           1    1    1 <- only one way for 10 to get to the next number [12]
            // countList  :                      2    1    1    1 <- 9 can get to [10,12] with JoltDifference = 3, add the counts together
            // countList  :                  3   2    1    1    1 <- 7 can get to [9,10] with JoltDifference = 3, add the counts together
            // countList  :              5   3   2    1    1    1 <- 6 can get to [7,9] with JoltDifference = 3, add the counts together
            // countList  :          5   5   3   2    1    1    1 <- only one way for 3 to get to the next number [6]
            // countList  :      5   5   5   3   2    1    1    1 <- only one way for 2 to get to the next number [3]
            // countList  : 10   5   5   5   3   2    1    1    1 <- 1 can get to [2,3] with JoltDifference = 3, add the counts together
            
            // Once we have the count list complete, consider the outlet. In this example, the outlet is 0 and can handle devices 
            // with a Jolt of [1,2,3], add the 3 counts together: Solution for this example would be 10 + 5 + 5 = 20.
            
                 
            // countList is used to hold the number of permutations from a specific adapter to the device.
            UInt64[] countList = new UInt64[adapterList.Length];

            // NOTE: The array of adapters is already sorted - done as part of the initialization.
            // Start at the end and count how many ways to get to the device
            for(int index1 = adapterList.Length; index1 > 0; index1--)
            {
                // We are at the top of the list, set the value to 1
                if(index1 == adapterList.Length)
                {
                    countList[index1 - 1] = 1;
                }
                else
                {
                    // For this specific adapter, check which adapters can be connected 
                    for(int index2 = index1; index2 < countList.Length; index2++)
                    {
                        if(adapterList[index2] - adapterList[index1-1] > aJoltDifference)
                        {
                            // Since the list is sorted, once the JoltDifference is exceeded, we don't have to
                            // continue searching anymore.
                            continue;
                        }
                        else
                        {
                            // We found an adapter that can be connected, add it's count to the current adapter
                            countList[index1 - 1] += countList[index2];
                        }
                    }
                }
            }

            UInt64 permutationCount = 0;

            // Add in the power source; if the adapter is within the JoltDifference, then add it's count to the
            // total number of permutations
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
