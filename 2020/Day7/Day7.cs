using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Day7
    {
        public struct Luggage
        {
            public int count;
            public string colour;

            public Luggage( int aCount, string aColour )
            {
                this.count = aCount;
                this.colour = aColour;
            }
        }

        public static Dictionary<string, List<Luggage>> luggageDictionary;

        public static void Main(string[] args)
        {
            luggageDictionary = buildLuggageDictionary(@"C:\Users\Paul Rodriguez\source\repos\ConsoleApp1\ConsoleApp1\Day7Input.txt");

            Console.WriteLine("Part 1 Answer: " + day7Part1("shiny gold"));
            Console.WriteLine("Part 2 Answer: " + day7Part2("shiny gold"));

            Console.ReadKey();
        }

        private static int day7Part1(string colour)
        {
            int count = 0;

            foreach (KeyValuePair<string, List<Luggage>> kvp in luggageDictionary)
            {
                if (luggageHoldsColour(colour, kvp.Value))
                {
                    count = count + 1;
                }

            }

            return count;

        }

        private static int day7Part2(string colour)
        {
            int count = 0;

            List<Luggage> aLuggageList;
            luggageDictionary.TryGetValue(colour, out aLuggageList);

            return luggageCountBags(aLuggageList);

        }

        private static bool luggageHoldsColour(string aColour, List<Luggage> luggageList)
        {
            if( luggageList != null)
            {
                foreach(Luggage aLuggage in luggageList )
                {
                    if( aLuggage.colour.Equals(aColour) )
                    {
                        return true;
                    }
                    else
                    {
                        List<Luggage> aLuggageList;
                        luggageDictionary.TryGetValue(aLuggage.colour, out aLuggageList);

                        if (luggageHoldsColour(aColour, aLuggageList))
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        private static int luggageCountBags(List<Luggage> luggageList)
        {
            int count = 0;

            if(luggageList != null)
            {
                foreach (Luggage aLuggage in luggageList)
                {
                    List<Luggage> aLuggageList;
                    luggageDictionary.TryGetValue(aLuggage.colour, out aLuggageList);

                    count += aLuggage.count * (1 + luggageCountBags(aLuggageList));
                }
            }

            return count;
        }

        private static Dictionary<string, List<Luggage>> buildLuggageDictionary(string fileName)
        {
            Dictionary<string, List<Luggage>> aDictionary = new Dictionary<string, List<Luggage>>();

            string fileText = System.IO.File.ReadAllText(fileName).Replace("\r\n", String.Empty);

            Regex rx1 = new Regex(@"([\w\s]+)\sbags\scontain\s(.*?)\.");

            MatchCollection mc1 = rx1.Matches(fileText);

            foreach( Match match1 in mc1)
            {
                GroupCollection gc1 = match1.Groups;

                if(gc1[2].Value.Equals("no other bags") )
                {
                    aDictionary.Add(gc1[1].Value, null);
                }
                else
                {
                    List<Luggage> containsList = new List<Luggage>();

                    Regex rx2 = new Regex(@"([\d]+)\s([\w\s]+)\s(bag|bags)");

                    MatchCollection mc2 = rx2.Matches(gc1[2].Value);

                    foreach( Match match2 in mc2 )
                    {
                        GroupCollection gc2 = match2.Groups;

                        containsList.Add( new Luggage(Int32.Parse(gc2[1].Value), gc2[2].Value) );
                    }

                    aDictionary.Add(gc1[1].Value, containsList);
                }
            }

            return aDictionary;
        }

        private static string printLuggageDictionary()
        {
            string aString = "";

            Console.WriteLine("DictionaryToString");

            foreach(KeyValuePair<string, List<Luggage>> kvp in luggageDictionary)
            {
                Console.WriteLine("Key : " + kvp.Key);
                if( kvp.Value == null )
                {
                    Console.WriteLine("Value : null");
                }
                else
                {
                    foreach( Luggage aLuggage in kvp.Value )
                    {
                        Console.WriteLine("Value : (count)" + aLuggage.count + " (colour)" + aLuggage.colour);
                    }
                }
            }

            return aString;
        }

    }
}
