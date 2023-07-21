using System.Diagnostics.Contracts;

namespace _03.Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ICallable callable;

            foreach (var phoneNumber in phoneNumbers)
            {
                if(phoneNumber.Length == 10)
                {
                    callable = new Smartphone();
                    Console.WriteLine(callable.Call(phoneNumber));
                }
                else
                {
                    callable = new StationaryPhone();
                    Console.WriteLine(callable.Call(phoneNumber));
                }
            }

            string[] sites = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IBrowsable browsable = new Smartphone();

            foreach (var site in sites)
            {
                Console.WriteLine(browsable.Browse(site));
            }
        }
    }
}