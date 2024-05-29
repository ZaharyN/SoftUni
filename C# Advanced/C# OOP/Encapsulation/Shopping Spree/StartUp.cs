namespace _3.ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            string[] personInfo = Console.ReadLine()
                .Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string pair in personInfo)
            {
                string[] personAndMoney = pair
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    Person person = new(personAndMoney[0], decimal.Parse(personAndMoney[1]));
                    people.Add(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            string[] productInfo = Console.ReadLine()
                .Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string pair in productInfo)
            {
                string[] productNameAndCost = pair
                    .Split('=', StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    Product product = new(productNameAndCost[0], decimal.Parse(productNameAndCost[1]));
                    products.Add(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Person currentPerson = people.FirstOrDefault(x => x.Name == commandArgs[0]);
                Product currentProdcut = products.FirstOrDefault(x => x.Name == commandArgs[1]);

                if(currentPerson is not null && currentProdcut is not null)
                {
                    Console.WriteLine(currentPerson.AddProduct(currentProdcut));
                }
            }
            foreach (var person in people)
            {
                if (person.Products.Count > 0)
                {
                    Console.WriteLine($"{person.Name} - {string.Join(", ", person.Products.Select(x => x.Name))}");
                }
                else
                {
                    Console.WriteLine($"{person.Name} - Nothing bought");
                }
            }
        }
    }
}