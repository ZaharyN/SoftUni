using System;
using System.Linq;

namespace _01._Reverse_array
{
	internal class Program
	{
		public static int[] array;
		public static bool isGenerated = false;
		static void Main(string[] args)
		{
			array = Console.ReadLine()
					.Split(" ", StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToArray();

			ReverseArray(0);
		}

		private static void ReverseArray(int index)
		{
			if(index >= array.Length / 2)
			{
                Console.WriteLine(string.Join(" ", array));
                return;
			}
			SwapPositions(index);
			ReverseArray(index + 1);
		}

		private static void SwapPositions(int i)
		{
			int temp = array[array.Length - 1 - i];
			array[array.Length - 1 - i] = array[i];
			array[i] = temp;
		}
	}
}
