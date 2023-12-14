namespace SumNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Func<int[], int> sum = x => x.Sum();
            Func<int[], int> length = Length;


            Console.WriteLine(length(input));
            Console.WriteLine(sum(input));
            //Console.WriteLine(input.Length);
            //Console.WriteLine(input.Sum());
        }

        //static int Sum(int[] input)
        //{
        //    int sum = 0;
        //    foreach (int element in input)
        //    {
        //        sum += element;
        //    }

        //    return sum;
        //}

        static int Length(int[] input)
        {
            int length = 0;

            foreach (var element in input)
            {
                length++;
            }
            return length;
        }
    }
}
