using System;
using System.Linq;
using System.Collections.Generic;

namespace Permutations_without_Repetitions
{
	internal class Program
	{
		public static char[] elements;
		public static char[] permutations;
		public static bool[] used;
		static void Main(string[] args)
		{
			elements = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(char.Parse)
				.ToArray();

			permutations = new char[elements.Length];
			used = new bool[elements.Length];

			GeneratePermutations(0);
		}

		private static void GeneratePermutations(int index)
		{
			if (index >= permutations.Length)
			{
				Console.WriteLine(string.Join(" ", permutations));
				return;
			}

			for (int i = 0; i < elements.Length; i++)
			{
				if (!used[i])
				{
					permutations[index] = elements[i];
					used[i] = true;
					GeneratePermutations(index + 1);

					used[i] = false;
				}
			}
		}
	}
}
