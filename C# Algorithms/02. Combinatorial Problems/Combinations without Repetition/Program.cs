using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Combinations_without_Repetition
{
	internal class Program
	{
		public static char[] elements;
		public static char[] combinations;
		static void Main(string[] args)
		{
			elements = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(char.Parse)
				.ToArray();

			int k = int.Parse(Console.ReadLine());
			combinations = new char[k];

			GenerateCombinations(0, 0);
		}

		private static void GenerateCombinations(int index, int elementsStartIndex)
		{
			if (index >= combinations.Length)
			{
				Console.WriteLine(string.Join(" ", combinations));
				return;
			}

			for (int i = elementsStartIndex; i < elements.Length; i++)
			{
				combinations[index] = elements[i];
				GenerateCombinations(index + 1, i + 1);
			}
		}
	}
}
