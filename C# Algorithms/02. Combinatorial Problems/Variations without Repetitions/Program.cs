using System;
using System.Linq;
using System.Collections.Generic;

namespace Variations_without_Repetitions
{
	internal class Program
	{
		public static char[] elements;
		public static char[] variations;
		public static bool[] used;
		static void Main(string[] args)
		{
			elements = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(char.Parse)
				.ToArray();

			int k = int.Parse(Console.ReadLine());

			variations = new char[k];
			used = new bool[elements.Length];

			GenerateVariations(0);
		}

		private static void GenerateVariations(int index)
		{
			if (index >= variations.Length)
			{
				Console.WriteLine(string.Join(" ", variations));
				return;
			}

			for (int i = 0; i < elements.Length; i++)
			{
				if (!used[i])
				{
					variations[index] = elements[i];
					used[i] = true;
					GenerateVariations(index + 1);

					used[i] = false;
				}
			}
		}
	}
}
