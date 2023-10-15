using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string animalType;

            while ((animalType = System.Console.ReadLine()) != "Beast!")
            {
                string[] animalInfo = System.Console.ReadLine()
                    .Split();

                try
                {
                    if (animalType == "Cat")
                    {
                        Cat cat = new Cat(animalInfo[0], int.Parse(animalInfo[1]), animalInfo[2]);
                        Console.WriteLine(animalType);
                        Console.WriteLine(cat.ToString());
                    }
                    else if (animalType == "Dog")
                    {
                        Dog dog = new Dog(animalInfo[0], int.Parse(animalInfo[1]), animalInfo[2]);
                        Console.WriteLine(animalType);
                        Console.WriteLine(dog.ToString());
                    }
                    else if (animalType == "Frog")
                    {
                        Frog frog = new Frog(animalInfo[0], int.Parse(animalInfo[1]), animalInfo[2]);
                        Console.WriteLine(animalType);
                        Console.WriteLine(frog.ToString());
                    }
                    else if (animalType == "Tomcat")
                    {
                        Tomcat tomcat = new Tomcat(animalInfo[0], int.Parse(animalInfo[1]));
                        Console.WriteLine(animalType);
                        Console.WriteLine(tomcat.ToString());
                    }
                    else if (animalType == "Kitten")
                    {
                        Kitten kitten = new Kitten(animalInfo[0], int.Parse(animalInfo[1]));
                        Console.WriteLine(animalType);
                        Console.WriteLine(kitten.ToString());
                    }
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
