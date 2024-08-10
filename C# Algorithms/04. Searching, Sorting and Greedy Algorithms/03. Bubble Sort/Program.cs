using System;
using System.Collections.Generic;
using System.Linq;

namespace Bubble_Sort
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			array = BubbleSort(array);
			Console.WriteLine(string.Join(" ", array));
		}

		private static int[] BubbleSort(int[] array)
		{
			bool isSorted = false;
			int i = 0;

			while (!isSorted)
			{
				isSorted = true;
				for (int j = 1; j < array.Length - i; j++)
				{
					if (array[j - 1] > array[j])
					{
						SwapPositions(array, j - 1, j);
						isSorted = false;
					}
				}
				i++;
			}

			return array;
		}

		private static void SwapPositions(int[] array, int biggerNumberIndex, int smallerNumberIndex)
		{
			int temp = array[biggerNumberIndex];
			array[biggerNumberIndex] = array[smallerNumberIndex];
			array[smallerNumberIndex] = temp;
		}
	}
}
