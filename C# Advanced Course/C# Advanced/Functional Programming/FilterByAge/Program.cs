using System.Threading.Channels;
using System.Xml.Schema;

namespace FilterbyAge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<Person> people = ReadPeople(n);

            string condition = Console.ReadLine();
            int ageTreshold = int.Parse(Console.ReadLine());
            string format = Console.ReadLine();

            Func<Person, bool> filter = CreateFilter(condition, ageTreshold);
            Action<Person> printer = CreatePrinter(format);
            PrintFilteredPeople(people, filter, printer);
        }
        static List<Person> ReadPeople(int n)
        {
            List<Person> people = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                string[] personInfo = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries);

                Person person = new Person()
                {
                    Name = personInfo[0],
                    Age = int.Parse(personInfo[1])
                };

                people.Add(person);
            }

            return people;
        }

        static Func<Person, bool> CreateFilter(string condition, int ageTreshold)
        {
            if (condition == "younger")
            {
                return p => p.Age < ageTreshold;
            }
            else if (condition == "older")
            {
                return p => p.Age >= ageTreshold;
            }
            return null;
        }

        static Action<Person> CreatePrinter(string format)
        {
            if (format == "name")
            {
                return p => Console.WriteLine(p.Name);
            }
            else if (format == "age")
            {
                return p => Console.WriteLine(p.Age);
            }
            else if (format == "name age")
            {
                return p => Console.WriteLine($"{p.Name} - {p.Age}");
            }
            return null;
        }

        static void PrintFilteredPeople(List<Person> people, Func<Person, bool> filter, Action<Person> printer)
        {
            people = people.Where(filter).ToList();

            foreach (var person in people)
            {
                printer(person);
            }
        }
    }
    public class Person
    {
        public string Name;
        public int Age;
    }
}
