using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Set_Cover_Greedy
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> universe = Console.ReadLine()
				.Split(", ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToList();

			int arrayCount = int.Parse(Console.ReadLine());

			HashSet<int[]> givenSets = new HashSet<int[]>();
			HashSet<int[]> selectedSets = new HashSet<int[]>();

			for (int i = 0; i < arrayCount; i++)
			{
				int[] currentSet = Console.ReadLine()
				.Split(", ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

				givenSets.Add(currentSet);
			}

			while(universe.Count > 0 && givenSets.Count > 0)
			{
				int[] selectedSet = givenSets
					.OrderByDescending(x => x.Count(
						s => universe.Contains(s)))
					.FirstOrDefault();

				selectedSets.Add(selectedSet);
				givenSets.Remove(selectedSet);

				universe = universe.Except(selectedSet).ToList();
			}

            Console.WriteLine($"Sets to take ({selectedSets.Count}):");
			foreach (var array in selectedSets)
			{
                Console.WriteLine(string.Join(", ", array));
			}
		}
	}
}
