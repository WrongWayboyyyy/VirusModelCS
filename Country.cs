﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VirusModel
{
    class Country
    {
        public readonly String Name;
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
            // TO ADD UpdateEconomic() and other
            foreach (var city in _cities)
            {
                city.Update(_season);
            }
        }

        public void UpdateEconomic()
        {
            _cities.Sort();
            
            foreach (var city in _cities)
            {
                //city.Budget += ModelConstants.CalculateBudget(city.IllnessFraction);
            }
        }

        private DateTime _time;
        private Season _season;
        private Double _budget;
        private List<City> _cities;

    }
}
