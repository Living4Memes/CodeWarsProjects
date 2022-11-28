public class Parser
{
      // Basic values and definition
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
      // Major values (like 100, 1 000, 1 000 000, etc.)
      public static Dictionary<string, int> MajorParsingValues = new Dictionary<string, int>()
      {
            { "million", 1000000 },
            { "thousand", 1000 },
            { "hundred", 100 },
      };

      // For debugging
      public static int ParseInt(string s)
      {
            Console.WriteLine($"Parsing: {s}");

            return UnwrappedParseInt(s);
      }

      // Main function (with implemented recursion)
      private static int UnwrappedParseInt(string s)
      {
            int result = 0;
            string parsingBase = s.Replace(" and ", " ");

            List<(string Quantifier, int Value)> quantifiedValues = SplitByMajor(parsingBase);

            foreach ((string Quantifier, int Value) currentPair in quantifiedValues)
            {
                  if (ContainsMajor(currentPair.Quantifier))
                        result += UnwrappedParseInt(currentPair.Quantifier) * currentPair.Value;
                  else if (!MajorParsingValues.ContainsKey(currentPair.Quantifier))
                  {
                        if (currentPair.Quantifier.Contains('-'))
                        {
                              string[] parts = currentPair.Quantifier.Split('-');
                              result += (ParsingValues[parts[0]] + ParsingValues[parts[1]]) * currentPair.Value;
                        }
                        else
                              result += ParsingValues[currentPair.Quantifier] * currentPair.Value;
                  }
            }

            return result;
      }

      // Splitting by major values
      private static List<(string Quantifier, int Value)> SplitByMajor(string s)
      {
            // (encoded quantifier, value) => eg.: (two hundred eighty-three, 1000)
            List<(string, int)> result = new List<(string, int)>();

            string tempString = s;
            // When number is described in words, all major numbers will be decreasing from start to end
            // So basically I'm splitting by biggest major, then by lesser, then by even more lesser, etc
            foreach(string majorValue in MajorParsingValues.Keys)
                  if (tempString.Contains(majorValue))
                  {
                        result.Add((tempString.Substring(0, tempString.LastIndexOf(majorValue) -1), MajorParsingValues[majorValue]));
                        tempString = tempString.Remove(0, tempString.LastIndexOf(majorValue) + majorValue.Length);

                        if(tempString.StartsWith(' '))
                              tempString = tempString.Remove(0, 1);
                  }

            if (tempString.Length > 0)
                  result.Add((tempString, 1));

            return result;
      }

      // Testing string if it contains major number for recursion
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
            Console.WriteLine(Parser.ParseInt("one hundred one"));
            Console.WriteLine(Parser.ParseInt("two hundred forty-six"));
            Console.WriteLine(Parser.ParseInt("seven hundred eighty-three thousand nine hundred and nineteen"));
            Console.WriteLine(Parser.ParseInt("seven hundred thousand"));
      }
}