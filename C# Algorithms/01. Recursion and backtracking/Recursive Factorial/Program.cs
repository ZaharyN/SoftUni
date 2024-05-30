using System;

namespace Recursive_Factorial
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());

			Console.WriteLine(CalculateFactorial(n));
		}

		public static int CalculateFactorial(int n)
		{
			if(n == 0) return 1;

			return n * CalculateFactorial(n - 1);
		}
	}
}
