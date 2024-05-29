namespace _10._Crossroads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int greenLightDuration = int.Parse(Console.ReadLine());
            int freeWindowDuration = int.Parse(Console.ReadLine());

            string input = string.Empty;

            Queue<string> cars = new Queue<string>();
            Queue<int> carLengths = new Queue<int>();
            int carsPassed = 0;

            while ((input = Console.ReadLine()) != "END")
            {
                int greenLightSeconds = greenLightDuration;
                int freeWindowSeconds = freeWindowDuration;
                int partsOnCrossroad = 0;

                if (input != "green")
                {
                    cars.Enqueue(input);

                    int currentLength = input.Length;
                    carLengths.Enqueue(currentLength);
                }
                else if (input == "green")
                {
                    while (cars.Any() && greenLightSeconds > 0)
                    {
                        if (greenLightSeconds - carLengths.Peek() >= 0)
                        {
                            greenLightSeconds -= carLengths.Dequeue();
                            carsPassed++;
                            cars.Dequeue();
                        }
                        else
                        {
                            partsOnCrossroad = carLengths.Peek() - greenLightSeconds;

                            if (freeWindowSeconds >= partsOnCrossroad)
                            {
                                carLengths.Dequeue();
                                carsPassed++;
                                cars.Dequeue();
                                greenLightSeconds = 0;
                            }
                            else if (freeWindowSeconds < partsOnCrossroad)
                            {
                                int crashSpot = greenLightSeconds + freeWindowSeconds;
                                Console.WriteLine("A crash happened!");
                                string currentCar = cars.Peek();
                                Console.WriteLine($"{currentCar} was hit at {currentCar[crashSpot]}.");
                                return;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{carsPassed} total cars passed the crossroads.");

        }
    }
}