using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
         
    public class Day9
    {

        private string inputFile;
        private int preambleLength;

        private string[] inputText;
        private int inputTextIndex;

        private Queue<UInt64> numberQueue;
        private UInt64 queueSum;

        public Day9(string anInputFile, int aPreamble)
        {
            Console.WriteLine("Starting Advent Of Code Day 9");

            this.inputFile = anInputFile;
            this.preambleLength = aPreamble;

            this.Initilize(this.preambleLength);
            RunDay9Part1();

            this.Initilize(0);
            RunDay9Part2(26796446);
        }

        private void Initilize(int initializeLength)
        {
            inputText = System.IO.File.ReadAllText(inputFile).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            inputTextIndex = 0;
            queueSum = 0;

            numberQueue = new Queue<UInt64>();

            for(inputTextIndex = 0; inputTextIndex < initializeLength; inputTextIndex++)
            {
                UInt64 anInt = UInt64.Parse(inputText[inputTextIndex]);
                AddNumberToQueue(anInt);
            }
        }

        private void AddNumberToQueue(UInt64 anInt)
        {
            queueSum += anInt;
            numberQueue.Enqueue(anInt);
        }

        private bool IsValidSum(UInt64 anInt)
        {
            UInt64[] numberArray = numberQueue.ToArray();

            for(int index = 0; index < numberArray.Length; index++)
            {
                if(numberQueue.Contains(anInt - numberArray[index]))
                {
                    return true;
                }
            }

            return false;
        }

        private UInt64 RemoveNumberFromQueue()
        {
            UInt64 anInt = numberQueue.Dequeue();
            queueSum -= anInt;
            return queueSum;
        }


        private void RunDay9Part1()
        {
            for( ; inputTextIndex < inputText.Length; inputTextIndex++ )
            {
                UInt64 anInt = UInt64.Parse(inputText[inputTextIndex]);

                if(!IsValidSum(anInt))
                {
                    Console.WriteLine("Day9Part1 : " + anInt);
                    break;
                }

                AddNumberToQueue(anInt);
                RemoveNumberFromQueue();
            }
        }

        private void RunDay9Part2(UInt64 matchSum)
        {
            while(this.queueSum != matchSum)
            {
                if(this.queueSum < matchSum)
                {
                    if (inputTextIndex < inputText.Length)
                    {
                        AddNumberToQueue(UInt64.Parse(inputText[inputTextIndex]));
                        inputTextIndex++;
                    }
                    else
                    {
                        Console.WriteLine("RunDay9Part2 : No more numbers");
                        break;
                    }
                }
                else
                {
                    RemoveNumberFromQueue();
                }

            }

            if(this.queueSum == matchSum)
            {
                UInt64[] numberArray = numberQueue.ToArray();
                Array.Sort(numberArray);

                UInt64 sumHighLow = numberArray[0] + numberArray[numberArray.Length - 1];
                Console.WriteLine("Day9Part2 : " + sumHighLow );
            }

        }
    }
}
