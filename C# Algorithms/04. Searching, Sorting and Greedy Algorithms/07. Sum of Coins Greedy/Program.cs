using System;
using System.Collections.Generic;
using System.Linq;

namespace Sum_of_Coins_Greedy
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Queue<int> coins = new Queue<int>(
				 Console.ReadLine()
				.Split(", ", StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.OrderByDescending(x => x));

			int target = int.Parse(Console.ReadLine());
			int totalCoins = 0;
			Dictionary<int, int> coinUsagePerValue = new Dictionary<int, int>();

			while (target > 0 && coins.Count > 0)
			{
				int currentCoin = coins.Dequeue();
				int coinUsageCount = target / currentCoin;

				if (coinUsageCount == 0)
				{
					continue;
				}
				totalCoins += coinUsageCount;
				coinUsagePerValue[currentCoin] = coinUsageCount;

				target %= currentCoin;
			}

			if (target == 0)
			{
				Console.WriteLine($"Number of coins to take: {totalCoins}");
				foreach (var coin in coinUsagePerValue)
				{
                    Console.WriteLine($"{coin.Value} coin(s) with value {coin.Key}");
				}
			}
			else
			{
                Console.WriteLine("Error");
			}
		}
	}
}
