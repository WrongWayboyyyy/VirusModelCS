using System;
using ConsoleApplication.Core;

namespace ConsoleApplication
{
    class Program
    {
        static void Main()
        {
            Country myCountry = new Country();
            while (true)
            {
                var nextQuery = Console.ReadLine().Split(' ');
                if (nextQuery[0].ToLower() == "add_city")
                {
                    String name = nextQuery[1];
                    
                    City myCity = new City(name);
                    bool ok = myCountry.AddCity(myCity);
                    if (!ok)
                    {
                        continue;
                    }
                    Console.WriteLine("Okay, enter the population of a city");
                    Int32 amount = Int32.Parse(System.Console.ReadLine());
                    for (var i = 0; i < amount; ++i)
                    {
                        Citizen newCitizen = new Citizen();
                        myCity.AddCitizen(newCitizen);
                    }
                    Console.WriteLine("Now enter initial number of infected people");
                    amount = Int32.Parse(System.Console.ReadLine());
                    myCity.InitializeInfection(amount);
                    Console.WriteLine("And finally, enter initial budget of a city");
                    Double money = Double.Parse(System.Console.ReadLine());
                    myCity.Budget = amount;
                    Console.WriteLine("---------------------------------------------");
                }
            
                if (nextQuery[0].ToLower() == "update")
                {
                    myCountry.UpdateCitiesState();
                    Console.WriteLine(myCountry);
                }
            }
        }
    }
}