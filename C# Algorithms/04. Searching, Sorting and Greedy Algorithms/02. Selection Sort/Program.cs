using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Selection_Sort
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			array = SelectionSort(array);
            Console.WriteLine(string.Join(" ", array));
        }

		private static int[] SelectionSort(int[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				int minNumberIndex = i;
				for (int j = i + 1; j < array.Length; j++)
				{
					if (array[j] < array[minNumberIndex])
					{
						minNumberIndex = j;
					}
				}
				SwapPositions(array, i, minNumberIndex);
			}

			return array;
		}

		private static void SwapPositions(int[] array, int index, int minNumberIndex)
		{
			int temp = array[index];
			array[index] = array[minNumberIndex];
			array[minNumberIndex] = temp;
		}
	}
}
