public class RailFenceCipher
{
      public static string Encode(string s, int n)
      {
            string[] rails = new string[n];

            for (int i = 0; i < n; i++)
            {
                  int index1 = (i - 1) * 2 + 1;
                  int index2 = (n -1 - i - 2) * 2 + 1;

                  if (index1 < 0)
                        index1 = 0;
                  if (index2 < 0)
                        index2 = 0;

                  rails[i] += s[index1];
                  rails[i] += s[index1 + index2 + 1];

                  int pointer = index1 + index2 + 2;

                  for (int j = 0; j < s.Length / n; j++)
                  {
                        if (index2 > 0)
                        {
                              pointer += index2;
                              if (pointer <= s.Length - 1)
                              { 
                                    rails[i] += s[pointer];
                                    pointer++;
                              }
                        }

                        if (index1 > 0)
                        {
                              pointer += index1;
                              if (pointer <= s.Length - 1)
                              {
                                    rails[i] += s[pointer];
                                    pointer++;
                              }
                        }
                  }
            }
            return string.Join(string.Empty, rails);
      }

      public static string Decode(string s, int n)
      {
            // Your code here
            return null;
      }
}

public static class Program
{
      public static void Main(string[] args)
      {
            Console.WriteLine();
            Console.WriteLine(RailFenceCipher.Encode("WEAREDISCOVEREDFLEEATONCE", 5));
            Console.WriteLine("WECRLTEERDSOEEFEAOCAIVDEN");
      }
}