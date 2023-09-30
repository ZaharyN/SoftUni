namespace _04.BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> buyers = new List<IBuyer>();
            int totalFood = 0;

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string[] buyerInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (buyerInfo.Length == 4)
                {
                    IBuyer buyer = new Citizen
                        (
                        buyerInfo[0],
                        int.Parse(buyerInfo[1]),
                        buyerInfo[2],
                        buyerInfo[3]
                        );

                    buyers.Add(buyer);
                }
                else if (buyerInfo.Length == 3)
                {
                    IBuyer buyer = new Rebel
                        (
                        buyerInfo[0],
                        int.Parse(buyerInfo[1]),
                        buyerInfo[2]
                        );

                    buyers.Add(buyer);
                }
            }

            string name;
            while ((name = Console.ReadLine()) != "End")
            {
                IBuyer currentBuyer = buyers.FirstOrDefault(x => x.Name == name);

                currentBuyer?.BuyFood();
            }
            Console.WriteLine(buyers.Sum(x => x.Food));
        }
    }
}