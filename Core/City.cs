﻿using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Linq;
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

        public void Vaccination(int amount)
        {
            Random rand = new Random();
            for (int i = 0; i < amount; ++i)
            {
                int sadForYou = rand.Next() % (_citizens.Count);
                while (_citizens[sadForYou].VaccinationStatus == true)
                {
                    sadForYou = rand.Next() % _citizens.Count;
                }
                _citizens[sadForYou].BecomeVaccinated();
            }
        }

        public void SpreadingInfection(Season season)
        {
            Double seasonRate = ModelConstants.SeasonConstant(season);
            Random rand = new Random();
            Double randomParameter1 = 0.3 + Math.Min(rand.NextDouble(), 0.7);
            Double randomParameter2 = 0.5 + Math.Min(rand.NextDouble(), 0.2);
            Double estimatedToBeInfected = IllnessFraction * ModelConstants.InfectionRate * randomParameter1
                                           * randomParameter2 * seasonRate * _population * _transportActivity;
            Int64 toBeInfected = Math.Min((int)estimatedToBeInfected, _citizens.Count - Infected);
            for (int i = 0; i < toBeInfected; ++i)
            {
                int sadForYou = rand.Next() % (_citizens.Count);
                
                _citizens[sadForYou].BecomeInfected();
            }
        }

        public City(String name = "City", Double transportActivity = 1)
        {
            _name = name;
            _transportActivity = transportActivity;
            _citizens = new List<Citizen>();
        }

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public Double Budget
        {
            get => _budget;
            set => _budget = value;
        }
        public Double IllnessFraction
        {
            get => (double) Infected / Population;
        }

        public void AddCitizen(Citizen newCitizen)
        {
            _citizens.Add(newCitizen);
        }

        public override string ToString()
        {
            string information = $"City {_name}, {_population} citizens, {Infected} infected, {Vaccinated} vaccinated, {_budget}$";
            return information;
        }

        public void InitializeInfection(int amount)
        {
            amount = Math.Min(amount, _citizens.Count);
            Random rand = new Random();
            Int32 idx = 0;
            for (int i = 0; i < amount; ++i)
            {
                while (_citizens[idx].WeeksToRecover > 0)
                {
                    idx = rand.Next(0, _citizens.Count);
                }
                _citizens[idx].BecomeInfected();
                
            }
        }
        
        private String _name;

        private Int64 _population
        {
            get => _citizens.Count;
        }
        

        public int Infected
        {
            get
            {
                var result = 0;
                foreach (var citizen in _citizens)
                {
                    if (citizen.WeeksToRecover > 0)
                    {
                        ++result;
                    }
                }

                return result;
            }
        }
        
        public int Vaccinated
        {
            get
            {
                var result = 0;
                foreach (var citizen in _citizens)
                {
                    if (citizen.VaccinationStatus == true)
                    {
                        ++result;
                    }
                }

                return result;
            }
        }
        
        private List<Citizen> _citizens;
        private readonly Double _transportActivity;
        private Double _budget;
        

    }
}