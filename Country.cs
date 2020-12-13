﻿using System;
using System.Collections.Generic;
 using System.Linq;
 using System.Text;

namespace VirusModel
{
    class Country
    {
        private readonly String _name;


        public Country(String name, DateTime time, Double budget)
        {
            _name = name;
            _time = time;
            _budget = budget;
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

        public void UpdateCityState()
        {
            UpdateEconomic();
            foreach (var city in _cities)
            {
                city.Update(_season);
            }
        }

        public void UpdateEconomic()
        {
            Double totalBudget = 0;
            foreach (var city in _cities)
            {
                totalBudget += city.Budget;
            }

            Double averageBudget = totalBudget / ModelConstants.TotalAmountOfCities;
            Double availableAvgBudget = ModelConstants.CountryIncomePerWeek / ModelConstants.TotalAmountOfCities;
            Double averageToSpend = System.Math.Min(averageBudget, availableAvgBudget);
            foreach (var city in _cities)
            {
                city.Budget += averageToSpend * (averageToSpend / city.Budget) * (1 + Math.Min(0, city.IllnessFraction) - 0.45);
            }

        }

        public void AddCity(City new_city)
        {
            _cities.Append(new_city);
        }

        private DateTime _time;
        private Season _season;
        private Double _budget;
        private List<City> _cities;
        

    }
}
