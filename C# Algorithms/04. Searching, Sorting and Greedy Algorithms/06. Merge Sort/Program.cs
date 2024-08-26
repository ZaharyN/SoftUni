using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Merge_Sort
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
				.Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			array = MergeSort(array);

			Console.WriteLine(string.Join(" ", array));
		}

		private static int[] MergeSort(int[] array)
		{
			if (array.Length <= 1)
			{
				return array;
			}

			int[] left = array.Take(array.Length / 2).ToArray();
			int[] right = array.Skip(array.Length / 2).ToArray();

			return MergeArrays(MergeSort(left), MergeSort(right));
		}

		private static int[] MergeArrays(int[] left, int[] right)
		{
			int[] sorted = new int[left.Length + right.Length];

			int startIndex = 0;
			int leftIndex = 0;
			int rightIndex = 0;

			while(leftIndex < left.Length && rightIndex < right.Length)
			{
				if (left[leftIndex] < right[rightIndex])
				{
					sorted[startIndex++] = left[leftIndex++];
				}
				else
				{
				 	sorted[startIndex++] = right[rightIndex++];	
				}
			}

			for (int i = leftIndex; i < left.Length; i++)
			{
				sorted[startIndex++] = left[i];
			}

			for (int i = rightIndex; i < right.Length; i++)
			{
				sorted[startIndex++] = right[i];
			}
			return sorted;
		}
	}
}
