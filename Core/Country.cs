﻿using System;
using System.Collections.Generic;
 using System.Linq;
 using System.Text;

namespace ConsoleApplication.Core
{
    class Country
    {
        private readonly String _name;

        
        public Country(String name = "Undefined", Double budget = 1_000_000_000)
        {
            _name = name;
            _budget = budget;
            _cities = new List<City>();
            _season = new Season();
            _time = ModelConstants.StartingDateTime;

        }

        public Int64 CitiesCount
        {
            get => _cities.Count;
        }

        public City this[int i]
        {
            get => _cities[i];
        }
        public Int64 Population
        {
            get
            {
                Int64 result = 0;
                foreach (var city in _cities)
                {
                    result += city.Population;
                }

                return result;
            }
        }
        public Double IllnessFraction
        {
            get
            {
                Double result = 0;
                foreach (var city in _cities)
                {
                    result += city.IllnessFraction;
                }
                result /= _cities.Count;
                return result;
            }
        }

        public void UpdateCitiesState()
        {
            UpdateEconomic();
            UpdateVaccination();
            foreach (var city in _cities)
            {
                city.Update(_season);
            }

            _time = _time.AddDays(7);
            if (_time.DayOfYear < 31 || _time.DayOfYear > 304)
            {
                _season = Season.Winter;
            }

            if (_time.DayOfYear > 31 && _time.DayOfYear < 153)
            {
                _season = Season.Spring;
            }

            if (_time.DayOfYear > 153 && _time.DayOfYear < 244)
            {
                _season = Season.Summer;
            }

            if (_time.DayOfYear > 244 && _time.DayOfYear < 304)
            {
                _season = Season.Autumn;
            }
        }

        public void UpdateVaccination()
        {
            Int64 totalInfected = 0;
            foreach (var city in _cities)
            {
                totalInfected += city.Infected;
            }

            foreach (var city in _cities)
            {
                double infectionFraction = (double) city.Infected / totalInfected;
                int estimatedVaccination = (int) (ModelConstants.AmountOfVaccinesPerWeek * infectionFraction);
                int toBeVaccinated = (int)(Math.Min(estimatedVaccination, city.Budget / ModelConstants.VaccinationCost));
                city.Vaccination(toBeVaccinated);
            }
        }

        public void UpdateEconomic()
        {
            Double totalBudget = 0;
            foreach (var city in _cities)
            {
                totalBudget += city.Budget;
            }

            Double averageBudget = totalBudget / _cities.Count;
            Double availableAvgBudget = ModelConstants.CountryIncomePerWeek / _cities.Count;
            foreach (var city in _cities)
            {
                city.Budget += availableAvgBudget * (averageBudget / city.Budget) * 
                               Math.Pow((1 + Math.Min(0, city.IllnessFraction - ModelConstants.PandemicThreshold)), 3);
            }

        }

        public override string ToString()
        {
            string result = $"{_time}, {_season}---------------------------------\n";
            int i = 1;
            foreach (var city in _cities)
            {
                
                result += $"{i++}.  {city.ToString()} \n";
            }

            result += "--------------------------------------------------\n";
            return result;
        }

        public bool AddCity(City newCity)
        {
            foreach (var city in _cities)
            {
                if (city.Name == newCity.Name)
                {
                    Console.WriteLine($"City {city.Name} already exists");
                    return false;
                }
            }
            Console.WriteLine($"City {newCity.Name} is successfully created");
            _cities.Add(newCity);
            return true;
        }

        private DateTime _time;
        private Season _season;
        private Double _budget;
        private List<City> _cities;
        private readonly DateTime _initialDate;

    }
}
