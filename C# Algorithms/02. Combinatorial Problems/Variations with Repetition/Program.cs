using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Variations_with_Repetition
{
	internal class Program
	{
		public static char[] elements;
		public static char[] variations;
		static void Main(string[] args)
		{
			elements = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(char.Parse)
				.ToArray();

			int k = int.Parse(Console.ReadLine());

			variations = new char[k];

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
				variations[index] = elements[i];
				GenerateVariations(index + 1);
			}
		}
	}
}
