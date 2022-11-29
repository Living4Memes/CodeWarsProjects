public class JomoPipi
{
      public static string StringFunc(string s, long x)
      {
            string result = s;
            long mod = FindMod(s);
            long correct = x % mod;
            Console.WriteLine($"Input string: [{s}]. Length: {s.Length}. Iterations: {x}. Mod: {mod}. Correct iterations: {correct}");
            for (long k = 0; k < correct; k++)
                  result = Reverse(result);
            return result;
      }

      public static string Reverse(string s)
      {
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
                  Array.Reverse(charArray, i, s.Length - i);
            return new string(charArray);
      }

      public static long FindMod(string s)
      {
            long result = 0;
            string temp = s;
            while(temp != s || result == 0)
            {
                  temp = Reverse(temp);
                  result++;
            }
            return result;
      }
}

public static class Program
{
      public static void Main(string[] args)
      {
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("a", 50)}]");
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("ab", 50)}]");
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("abc", 50)}]");
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("abcde", 50)}]");
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("abcdef", 50)}]");
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("abcdefg", 50)}]");
            Console.WriteLine($"Actual:  [{JomoPipi.StringFunc("abcdefgh", 50)}]");
      }
}