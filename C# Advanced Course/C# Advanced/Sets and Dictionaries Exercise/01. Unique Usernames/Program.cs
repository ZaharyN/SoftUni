﻿namespace _01._Unique_Usernames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> names = new HashSet<string>();

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string name = Console.ReadLine();
                names.Add(name);
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}