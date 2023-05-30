using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Base91
{
	private const string VOCAB = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&()*+,./:;<=>?@[]^_`{|}~\"";
	
	public static string Encode(string input)
	{
		string result = string.Empty;
		BitArray bits = new BitArray(Encoding.UTF8.GetBytes(input));
		bits = new BitArray(bits.Cast<bool>().ToArray());
		Console.Write("Encoded:");
		WriteBits(bits.Cast<bool>().ToArray());
		int sectorCount = bits.Length / 13;
		if (bits.Length % 13 > 0)
			sectorCount++;

		for(int i = 0; i < sectorCount; i++)
            {
			BitArray currentSector = new BitArray(bits.Cast<bool>().Skip(i * 13).Take(13).ToArray());
			int value = GetInt(currentSector);
			result += $"{VOCAB[value % 91]}{VOCAB[value / 91]}";
		}

		return result;
	}

	public static string Decode(string input)
	{
            List<bool[]> sectors = new List<bool[]>();
		string result = string.Empty;

		for (int i = 0; i < input.Length / 2; i++)
			sectors.Add(GetBits(new string(input.Skip(i * 2).Take(2).ToArray())));

		bool[] allBools = sectors.SelectMany(x => x).ToArray();
		Console.Write("Decoded:");
		WriteBits(allBools);
		//result = Encoding.UTF8.GetString(bytes);

		return result;
	}
	private static int GetInt(BitArray bitArray)
	{
		int[] array = new int[1];
		bitArray.CopyTo(array, 0);
		return array[0];
	}
	private static bool[] GetBits(string s)
      {
		if (s.Length != 2)
			throw new ArgumentException("Wrong input", nameof(s));

		bool[] bools = GetBools(Convert.ToString(VOCAB.IndexOf(s[0]) * 13 + VOCAB.IndexOf(s[1]), 2)).ToArray();

		return bools;
	}
	private static IEnumerable<bool> GetBools(string s)
      {
		string actual = string.Concat(new string('0', 13 - s.Length), s);
		for (int i = 0; i < actual.Length; i++)
			yield return actual[i] == '1';
      }
	private static bool[] FixBools(bool[] bools)
      {
		if (bools.Length % 13 == 0)
			return bools;

		bool[] correct = new bool[bools.Length + 13 - bools.Length % 13];
		Array.Copy(bools, correct, 13 - bools.Length % 13);

		return correct;
      }
	private static void WriteBits(IEnumerable<bool> bits)
      {
		foreach (bool b in bits)
			if (b)
				Console.Write(1);
			else
				Console.Write(0);

		Console.WriteLine();
      }
}

public class Program
{
	public static void Main(string[] args)
      {
		//Base91.Encode("Hello world!");
		//Base91.Decode(">OwJh>Io0Tv!8PE");
		//Base91.Encode("test");
		//Base91.Decode("fPNKd");

		Console.WriteLine(Base91.Encode("Hello world!"));
		Console.WriteLine(Base91.Decode(">OwJh>Io0Tv!8PE"));
		//Console.WriteLine(Base91.Encode("test"));
		//Console.WriteLine(Base91.Decode("fPNKd"));

	}
}