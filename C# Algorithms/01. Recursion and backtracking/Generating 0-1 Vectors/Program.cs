
namespace Generating_0_1_Vectors
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());

			int[] array = new int[n];

			GenerateVector(array, 0);
		}

		private static void GenerateVector(int[] array, int index)
		{
			if(index >= array.Length)
			{
				Console.WriteLine(string.Join("", array));
				return;
			}

			for (int i = 0; i < 2; i++)
			{
				array[index] = i;

				GenerateVector(array, ++index);
				index--;
            }
		}
	}
}
