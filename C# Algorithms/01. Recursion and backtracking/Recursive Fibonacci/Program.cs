using System;
using System.Linq;
using System.Collections.Generic;

namespace Recursive_Fibonacci
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int index = int.Parse(Console.ReadLine());

            Console.WriteLine(CalculateFibonacci(index));

		}

		private static long CalculateFibonacci(int index)
		{
			if(index <= 1)
			{
				return 1;
			}

			return CalculateFibonacci(index - 1) + CalculateFibonacci(index - 2);
		}
	}
}
