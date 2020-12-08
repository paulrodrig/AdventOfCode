using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day6
    {
        //static void Main(string[] args)
        //{
        //    string inputText = readInputFile(@"C:\Users\Paul Rodriguez\source\repos\ConsoleApp1\ConsoleApp1\day6Input.txt");

        //    Console.WriteLine("Part 1 Answer: " + day6Part1(inputText));
        //    Console.WriteLine("Part 2 Answer: " + day6Part2(inputText));

        //    Console.ReadKey();
        //}

        private static int day6Part1(string inputString)
        {
            int answer = 0;

            string[] groups = inputString.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string aString in groups)
            {
                string groupString = aString.Replace("\r\n", String.Empty);

                var results = groupString.ToCharArray().Distinct().ToArray();

                answer = answer + results.Length;

            }

            return answer;

        }

        private static int day6Part2(string inputString)
        {
            int answer = 0;

            string[] groups = inputString.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string aString in groups)
            {
                string[] individualDeclaration = aString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (individualDeclaration.Length == 1)
                {
                    answer = answer + individualDeclaration[0].Replace("\r\n", String.Empty).Length;
                }
                else
                {
                    char[] intersect = { };

                    for(int index=0; index < individualDeclaration.Length; index++)
                    {
                        if( index > 0)
                        {
                            IEnumerable<char> temp = intersect.Intersect(individualDeclaration[index].ToCharArray());

                            intersect = temp.ToArray();
                        }
                        else
                        {
                            intersect = individualDeclaration[index].Replace("\r\n", String.Empty).ToCharArray();
                        }
                    }

                    answer = answer + intersect.Length;

                }

            }

            return answer;

        }

        private static string readInputFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }
    }
}
