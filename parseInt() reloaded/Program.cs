public class Parser
{
      public static Dictionary<string, int> ParsingValues = new Dictionary<string, int>()
      {
            { "ninety", 90},
            { "eighty", 80},
            { "seventy", 70},
            { "sixty", 60},
            { "fifty", 50},
            { "forty", 40},
            { "thirty", 30},
            { "twenty", 20},
            { "nineteen", 19},
            { "eighteen", 18},
            { "seventeen", 17},
            { "sixteen", 16},
            { "fifteen", 15 },
            { "fourteen", 14},
            { "thirteen", 13 },
            { "twelve", 12 },
            { "eleven", 11},
            { "ten", 10},
            { "nine", 9},
            { "eight", 8},
            { "seven", 7},
            { "six", 6},
            { "five", 5},
            { "four", 4},
            { "three", 3},
            { "two", 2},
            { "one", 1},
            { "zero", 0},
      };
      public static Dictionary<string, int> MajorParsingValues = new Dictionary<string, int>()
      {
            { "million", 1000000 },
            { "thousand", 1000 },
            { "hundred", 100 },
      };

      public static int ParseInt(string s)
      {
            Console.WriteLine($"Parsing: {s}");

            return WrappedParseInt(s);
      }

      private static int WrappedParseInt(string s)
      {
            int result = 0;
            string parsingBase = s.Replace(" and ", " ");

            Dictionary<string, int> quantifiedValues = SplitByMajor(parsingBase);


            foreach (string quantifier in quantifiedValues.Keys)
            {
                  if (ContainsMajor(quantifier))
                        result += ParseInt(quantifier) * quantifiedValues[quantifier];
                  else if (!MajorParsingValues.ContainsKey(quantifier))
                  {
                        if (quantifier.Contains('-'))
                        {
                              string[] parts = quantifier.Split('-');
                              result += (ParsingValues[parts[0]] + ParsingValues[parts[1]]) * quantifiedValues[quantifier];
                        }
                        else
                              result += ParsingValues[quantifier] * quantifiedValues[quantifier];
                  }
            }

            return result;
      }

      private static Dictionary<string, int> SplitByMajor(string s)
      {
            // eg.: <two hundred eighty-three, 1000> (<encoded quantifier, value>)
            Dictionary<string, int> result = new Dictionary<string, int>();

            string tempString = s;
            foreach(string majorValue in MajorParsingValues.Keys)
                  if (s.Contains(majorValue))
                  {
                        result.Add(tempString.Substring(0, tempString.LastIndexOf(majorValue) -1), MajorParsingValues[majorValue]);
                        tempString = tempString.Remove(0, tempString.LastIndexOf(majorValue) + majorValue.Length);

                        if(tempString.StartsWith(' '))
                              tempString = tempString.Remove(0, 1);
                  }

            if (tempString.Length > 0)
                  result.Add(tempString, 1);

            return result;
      }

      private static bool ContainsMajor(string s)
      {
            foreach (string major in MajorParsingValues.Keys)
                  if (s.Contains(major))
                        return true;
            return false;
      }
}

public class Program
{
      public static void Main(string[] args)
      {
            Console.WriteLine(Parser.ParseInt("four"));
            Console.WriteLine(Parser.ParseInt("one hundred"));
            Console.WriteLine(Parser.ParseInt("two hundred forty-six"));
            Console.WriteLine(Parser.ParseInt("seven hundred eighty-three thousand nine hundred and nineteen"));
      }
}