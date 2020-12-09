﻿using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Text;

namespace VirusModel
{
    class City : IComparable
    {
        public Int64 Population => _population;

        public void UpdateEconomics()
        {
            foreach (var citizen in _citizens)
            {
                if (!(citizen.WeeksToRecover > 0))
                {
                    _budget += ModelConstants.TaxRate * citizen.Salary;
                }
                else
                {
                    _budget -= ModelConstants.DisabilityPayment;
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            City otherCity = obj as City;
            if (otherCity != null)
            {
                return this.IllnessFraction.CompareTo(otherCity.IllnessFraction);
            }
            else
            {
                throw new ArgumentException("Object is not a City");
            }
        }

        public void Update(Season season)
        {
            foreach (var citizen in _citizens)
            {
                citizen.Update();
            }
            SpreadingInfection(season);
        }

        public void SpreadingInfection(Season season)
        {
            Double seasonRate = ModelConstants.SeasonConstant(season);
            Random rand = new Random();
            Double randomParameter1 = rand.Next();
            Double randomParameter2 = rand.Next();
            double estimatedToBeInfected = _infected * ModelConstants.InfectionRate * randomParameter1
                                           * randomParameter2 * seasonRate * _population;
            Int64 toBeInfected = (int)estimatedToBeInfected;
            for (int i = 0; i < toBeInfected; ++i)
            {
                int sadForYou = rand.Next();
                while (_citizens[sadForYou].VaccinationStatus == true)
                {
                    sadForYou = rand.Next();
                }
                _citizens[sadForYou].BecomeInfected();
            }
        }

        public City(String name, Int64 population, Double transportActivity, Int64 initialInfection)
        {
            Name = name;
            _population = population;
            _transportActivity = transportActivity;
            _infected = initialInfection;
        }
        public readonly String Name;
        public Double Budget
        {
            get => _budget;
            set => _budget = value;
        }
        public Double IllnessFraction
        {
            get => (double)_population / _infected;
        }
        
        private List<Citizen> _citizens;
        private Int64 _population;
        private readonly Double _transportActivity;
        private Double _budget;
        private Int64 _infected;

    }
}