using System;
using System.Linq;
using System.Collections.Generic;

namespace Permutations_with__Repetition_Reduced_Complexity
{
	internal class Program
	{
		public static char[] elements;
		static void Main(string[] args)
		{
			elements = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(char.Parse)
				.ToArray();

			GeneratePermutations(0);
		}

		private static void GeneratePermutations(int index)
		{
			if (index >= elements.Length)
			{
				Console.WriteLine(string.Join(" ", elements));
				return;
			}

			GeneratePermutations(index + 1);

			for (int i = index + 1; i < elements.Length; i++)
			{
				SwapPlaces(index, i);
				GeneratePermutations(index + 1);
				SwapPlaces(index, i);
			}
		}

		private static void SwapPlaces(int currentNumberIndex, int nextNumber)
		{
			char temp = elements[currentNumberIndex];

			elements[currentNumberIndex] = elements[nextNumber];
			elements[nextNumber] = temp;
		}
	}
}
