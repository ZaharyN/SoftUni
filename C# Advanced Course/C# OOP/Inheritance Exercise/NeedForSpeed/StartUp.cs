using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SportCar sportCar = new(240, 50);

            Console.WriteLine(sportCar.FuelConsumption);
            Console.WriteLine(sportCar.Fuel);

            sportCar.Drive(30);

            Console.WriteLine(sportCar.Fuel);
        }
    }
}
