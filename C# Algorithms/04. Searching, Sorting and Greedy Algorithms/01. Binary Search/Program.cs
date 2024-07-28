using System;
using System.Collections.Generic;
using System.Linq;

namespace Binary_Search
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			int numberToFind = int.Parse(Console.ReadLine());

			int numberToFindIndex = BinarySearch(array, numberToFind);
			Console.WriteLine(numberToFindIndex);
		}

		private static int BinarySearch(int[] array, int numberToFind)
		{
			int leftIndex = 0;
			int rightIndex = array.Length - 1;

			while (leftIndex <= rightIndex)
			{
				int midIndex = (leftIndex + rightIndex) / 2;

				if (array[midIndex] == numberToFind)
				{
					return midIndex;
				}
				if (array[midIndex] > numberToFind)
				{
					rightIndex = midIndex - 1;
				}
				else if (array[midIndex] < numberToFind)
				{
					leftIndex = midIndex + 1;
				}
			}
			return -1;
		}
	}
}
