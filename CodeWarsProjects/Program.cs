using System;

public class SumOfSquares
{
      public static int NSquaresFor(int n)
      {
            return GetPerfectSquare(n);
      }

      public static int GetPerfectSquare(int n, int l = 0)
      {
            int ps = (int)Math.Floor(Math.Sqrt(n));
            int ps2 = ps ^ 2;
            Console.WriteLine(ps2);
            if (n >= ps2)
                  return GetPerfectSquare(n - ps2, ++l);
            else
                  return l;
      }
}