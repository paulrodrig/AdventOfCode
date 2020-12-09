using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
         
    public class Assembler
    {
        public enum Operation
        {
            NOP,
            ACC,
            JMP,
            UKN
        }

        public struct AssemblyOperation
        {
            public Operation assemblyOperation;
            public int assemblyValue;
            public int executionCount;

            public AssemblyOperation(Operation anOperation, int anAssemblyValue, int anExecutionCount)
            {
                assemblyOperation = anOperation;
                assemblyValue = anAssemblyValue;
                executionCount = anExecutionCount;
            }
        }

        private AssemblyOperation[] listOperations;
        private int intGlobalCounter;
        private int intOperationIndex;

        public Assembler(string inputFileName)
        {
            // Read the input file and split on newline character
            string[] inputFileText = System.IO.File.ReadAllText(inputFileName).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Initialize class variables
            this.listOperations = new AssemblyOperation[inputFileText.Length];

            // Initialize the operations
            InitilizeListOperations(inputFileText);

            // Reset all counters
            this.Reset();
        }

        public AssemblyOperation GetAssemblyOperation(int index)
        {
            return listOperations[index];
        }

        public int GetAssemblyOperationCount()
        {
            return listOperations.Length;
        }

        public int GetGlobalCounter()
        {
            return intGlobalCounter;
        }

        public int GetOperationIndex()
        {
            return intOperationIndex;
        }

        public int GetOperationExecutionCount()
        {
            return listOperations[intOperationIndex].executionCount;
        }

        public bool GetTerminated()
        {
            if (intOperationIndex < listOperations.Length)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            this.intGlobalCounter = 0;
            this.intOperationIndex = 0;

            for(int index = 0; index < listOperations.Length; index++)
            {
                listOperations[index].executionCount = 0;
            }

        }

        public void SetAssemblyOperation(AssemblyOperation anOperation, int index)
        {
            listOperations[index] = anOperation;
        }

        public void ExecuteOperation()
        {
            // Ensure program has not terminated
            if(GetTerminated())
            {
                Console.WriteLine("Program has terminated");
                return;
            }

            // Increase the execution counter
            listOperations[intOperationIndex].executionCount += 1;

            switch (listOperations[intOperationIndex].assemblyOperation)
            {
                case Operation.NOP:
                    // No action, increase the operation index
                    intOperationIndex += 1;
                    break;
                case Operation.ACC:
                    // Increase the global counter and increase the operation index
                    intGlobalCounter += listOperations[intOperationIndex].assemblyValue;
                    intOperationIndex += 1;
                    break;
                case Operation.JMP:
                    // Set global counter to the value relative to itself
                    intOperationIndex += listOperations[intOperationIndex].assemblyValue;
                    break;
                default:
                    break;
            }
        }

        private void InitilizeListOperations(string[] inputFileText)
        {
            for(int index = 0; index < listOperations.Length; index++ )
            {
                string aLine = inputFileText[index];
                // Break each line to 3 parts:
                //    [0,2] - 3 Character Operation Code
                //    [4]   - Positive/Negative Indicator
                //    [5,]  - Integer

                string operationCode = aLine.Substring(0, 3).ToUpper();
                int operationValue = Int32.Parse(aLine.Substring(5, aLine.Length - 5));

                if (aLine.Substring(4,1).Equals("-"))
                {
                    operationValue = operationValue * -1;
                }

                switch(operationCode)
                {
                    case "NOP":
                        listOperations[index] = new AssemblyOperation(Operation.NOP, operationValue, 0);
                        break;
                    case "ACC":
                        listOperations[index] = new AssemblyOperation(Operation.ACC, operationValue, 0);
                        break;
                    case "JMP":
                        listOperations[index] = new AssemblyOperation(Operation.JMP, operationValue, 0);
                        break;
                    default:
                        listOperations[index] = new AssemblyOperation(Operation.UKN, operationValue, 0);
                        break;
                }
            }
        }

    }
}
