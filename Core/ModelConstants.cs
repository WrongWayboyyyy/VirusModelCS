using System;

namespace ConsoleApplication.Core
{
    public static class ModelConstants
    {
        public static readonly Double TaxRate = 0.15;
        public static readonly Double DisabilityPayment = 8278;
        public static readonly Double InfectionRate = 8.5;
        public static readonly Double TwoWeeksIllnessRate = 0.6;
        public static readonly Double ThreeWeeksIllnessRate = 0.15;
        public static readonly Double OneWeekIllnessRate = 0.25;
        public static readonly Int64 ImmunityWeeksDuration = 24;
        public static readonly Int64 VaccinationDuration = 6;
        public static readonly Double CountryIncomePerWeek = 1_000_000;
        public static readonly Double PandemicThreshold = 0.45;
        public static readonly Double DefaultSalary = 43400;
        public static readonly Double VaccinationCost = 5000;
        public static readonly Int64 AmountOfVaccinesPerWeek = 100_000;
        public static readonly DateTime StartingDateTime = new DateTime(2020, 9, 1);
        

        public static Double SeasonConstant(Season season)
        {
            if (season == Season.Autumn || season == Season.Spring)
            {
                return 0.3;
            }

            if (season == Season.Winter)
            {
                return 0.35;
            }

            if (season == Season.Summer)
            {
                return 0.15;
            }

            return 0;
        }
    }
}