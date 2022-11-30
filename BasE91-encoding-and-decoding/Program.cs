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
		List<BitArray> sectors = new List<BitArray>();
		string result = string.Empty;

		for(int i = 0; i < input.Length / 2; i++)
            {
			string currentSector = new string(input.Skip(i * 2).Take(2).ToArray());
			sectors.Add(new BitArray(VOCAB.IndexOf(currentSector[0]) * 13 + VOCAB.IndexOf(currentSector[1])));
            }

		List<List<bool>> list = sectors.Select(x => x.Cast<bool>().ToList()).ToList();
		Console.WriteLine(String.Join("\n", list[0]));
		BitArray resultBits = new BitArray(list.Select(x => x.Skip(x.IndexOf(true) - 1)).SelectMany(x => x).ToArray());

		byte[] message = new byte[resultBits.Length / 8];
		resultBits.CopyTo(message, 0);

		result = Encoding.UTF8.GetString(message);

		return result;
	}

	private static int GetInt(BitArray bitArray)
	{
		int[] array = new int[1];
		bitArray.CopyTo(array, 0);
		return array[0];
	}
}

public class Program
{
	public static void Main(string[] args)
      {
		Console.WriteLine(Base91.Encode("Hello world!"));
		Console.WriteLine(Base91.Decode(">OwJh>Io0Tv!8PE"));
      }
}