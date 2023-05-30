using System.Diagnostics;

public class Kata
{
      public static long NextBiggerNumber(long n)
      {
            string nStr = n.ToString();

            if (nStr.Distinct().Count() < 2)
                  return -1;

            for (int i = nStr.Length - 1; i >= 0; i--)
                  for (int j = i; j >= 0; j--)
                  {
                        string tempStr = nStr;

                        long tempN = long.Parse(SwapDigits(tempStr, j, i));
                        if (tempN > n)
                              return tempN;
                  }

            return -1;
      }

      private static string SwapDigits(string initial, int a, int b)
      {
            if(a == b)
                  return initial;

            string result = initial.Substring(0, a);
            result += initial[b];
            result += initial.Substring(a + 1, b - a - 1);
            result += initial[a];
            result += initial.Substring(b + 1);

            return result;
      }

      public IEnumerable<char> GetAvaliableDigits(long n)
      {
            return n.ToString()
                  .ToCharArray()
                  .Distinct()
                  .OrderByDescending(x => x);
      }
}

public class Program
{
      public static void Main(string[] args)
      {
            while(true)
                  Console.WriteLine(Kata.NextBiggerNumber(long.Parse(Console.ReadLine())));

            
      }
}