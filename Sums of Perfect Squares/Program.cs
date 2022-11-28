// Incomplete
public class SumOfSquares
{
      public static int NSquaresFor(int n)
      {
            Dictionary<int, int> possible = GetPossibleSquares(n).ToDictionary(x => x.Square, x => x.Value);



            return 0;
      }

      public static IEnumerable<(int Value, int Square)> GetPossibleSquares(int n)
      {
            int k = 1;
            int k2 = 1;
            while (k2 < n)
            {
                  yield return (k, k2);
                  k++;
                  k2 = k * k;
            }
            yield break;
      }
}

internal class Program
{
      static void Main(string[] args)
      {
            Console.WriteLine(SumOfSquares.NSquaresFor(18));
      }
}
