using System;

namespace VirusModel
{
    public static class ModelConstants
    {
        public static readonly Double TaxRate = 0.15;
        public static readonly Double DisabilityPayment = 8278;
        public static readonly Double InfectionRate = 0.3;
        public static readonly Double TwoWeeksIllnessRate = 0.6;
        public static readonly Double ThreeWeeksIllnessRate = 0.15;
        public static readonly Double OneWeekIllnessRate = 0.25;
        public static readonly Int64 ImmunityWeeksDuration = 12;
        public static readonly Int64 VaccinationDuration = 3;
        public static readonly Double CountryIncomePerWeek = 100_000_000;
        public static readonly Int64 TotalAmountOfCities = 13;
        public static readonly Int64 TotalAmountOfCitizens = 10_000_000;
        public static readonly Double PandemicThreshold = 45;
        public static readonly Double DefaultSalary = 100000;

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