using System;
using System.Collections.Generic;
using System.Linq;

namespace Insertion_Sort
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			array = InsertionSort(array);
			Console.WriteLine(string.Join(" ", array));
		}

		private static int[] InsertionSort(int[] array)
		{
			for (int i = 1; i < array.Length; i++)
			{
				while (i > 0 && array[i] < array[i - 1])
				{
					SwapPositions(array, i, i - 1);
					i--;
				}
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
