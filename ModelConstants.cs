using System;

namespace VirusModel
{
    public static class ModelConstants
    {
        public static Double TaxRate = 0.15;
        public static Double DisabilityPayment = 8278;
        public static Double InfectionRate = 0.3;
        public static Double TwoWeeksIllnessRate = 0.6;
        public static Double ThreeWeeksIllnessRate = 0.15;
        public static Double OneWeekIllnessRate = 0.25;
        public static Int64 ImmunityWeeksDuration = 12;
        public static Int64 VaccinationDuration = 3;
        public static Double CountryIncomePerWeek = 1_000_000_000;

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