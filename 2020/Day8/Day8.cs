using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
         
    public class Day8
    {

        public Day8(string inputFileName)
        {
            Console.WriteLine("Starting Advent Of Code Day 8");

            Assembler anAssembler = new Assembler(inputFileName);
            
            RunDay8Part1(anAssembler);

            anAssembler.Reset();

            RunDay8Part2(anAssembler);

        }


        private void RunDay8Part1(Assembler anAssembler)
        {
            while (anAssembler.GetOperationExecutionCount() == 0)
            {
                anAssembler.ExecuteOperation();
            }

            Console.WriteLine("Day 8 Part 1: " + anAssembler.GetGlobalCounter());
        }

        private void RunDay8Part2(Assembler anAssembler)
        {
            for (int index = 0; index < anAssembler.GetAssemblyOperationCount()-1; index++)
            {
                anAssembler.Reset();

                Assembler.AssemblyOperation originalOperation = anAssembler.GetAssemblyOperation(index);

                Assembler.AssemblyOperation newOperation = originalOperation;

                switch(originalOperation.assemblyOperation)
                {
                    case Assembler.Operation.NOP:
                        newOperation.assemblyOperation = Assembler.Operation.JMP;
                        break;
                    case Assembler.Operation.JMP:
                        newOperation.assemblyOperation = Assembler.Operation.NOP;
                        break;
                    default:
                        continue;
                }

                anAssembler.SetAssemblyOperation(newOperation, index);

                while(anAssembler.GetTerminated() == false)
                {
                    if( anAssembler.GetOperationExecutionCount() == 0)
                    {
                        anAssembler.ExecuteOperation();
                    }
                    else
                    {
                        break;
                    }
                }

                if (anAssembler.GetTerminated() == true)
                {
                    Console.WriteLine("Day 8 Part 2: " + anAssembler.GetGlobalCounter());
                    break;
                }

                anAssembler.SetAssemblyOperation(originalOperation, index);
            }

        }

    }
}
