using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Quick_Sort
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			QuickSort(array, 0, array.Length - 1);
			Console.WriteLine(string.Join(" ", array));
		}

		private static void QuickSort(int[] array, int start, int end)
		{
			if (start >= end)
			{
				return;
			}
			int pivot = start;
			int left = start + 1;
			int right = end;

			while (left <= right)
			{
				if (array[left] > array[pivot] &&
					array[right] < array[pivot])
				{
					Swap(array, left, right);
				}
				if (array[left] <= array[pivot])
				{
					left += 1;
				}
				if (array[right] >= array[pivot])
				{
					right -= 1;
				}
			}
			Swap(array, pivot, right);
			QuickSort(array, start, right);
			QuickSort(array, left, end);
		}

		private static void Swap(int[] array, int left, int right)
		{
			int temp = array[left];
			array[left] = array[right];
			array[right] = temp;
		}
	}
}