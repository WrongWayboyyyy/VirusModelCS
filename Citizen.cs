using System;

namespace VirusModel
{
    public class Citizen
    {
        Citizen(String name, String surname, String midname, String birthday, Double salary)
        {
            Name = name;
            Surname = surname;
            Midname = midname;
            Birthday = birthday;
            Salary = salary;
            _weeksToBeVaccinated = -1;
            _weeksToRecover = 0;
        }

        public void BecomeInfected()
        {
            Random rand = new Random();
            int recoverRate = rand.Next() % 100;
            if (recoverRate < ModelConstants.OneWeekIllnessRate)
            {
                WeeksToRecover = 1;
            }
            else if (recoverRate < ModelConstants.TwoWeeksIllnessRate)
            {
                WeeksToRecover = 2;
            }
            else
            {
                WeeksToRecover = 3;
            }
            _weeksToBeVaccinated = _weeksToRecover;
        }

        public void BecomeVaccinated()
        {
            _weeksToBeVaccinated = ModelConstants.VaccinationDuration;
        }

        public void Update()
        {
            UpdateVaccinationStatus();
            UpdateInfectionStatus();
        }
        
        private void UpdateVaccinationStatus()
        {
            if (_weeksToBeVaccinated > 0)
            {
                --_weeksToBeVaccinated;
            }

            if (_weeksToBeVaccinated == 0)
            {
                VaccinationStatus = true;
                _weeksToRecover = -12;
            }
        }

        private void UpdateInfectionStatus()
        {
            if (_weeksToRecover > 0)
            {
                _weeksToRecover--;
                if (_weeksToRecover == 0)
                {
                    _weeksToRecover = ModelConstants.ImmunityWeeksDuration;
                }
            }
            else if (_weeksToRecover < 0)
            {
                _weeksToRecover++;
                if (_weeksToRecover == 0)
                {
                    VaccinationStatus = false;
                }
            }
        }
        public Int64 WeeksToRecover
        {
            get => _weeksToRecover;
            set => _weeksToRecover = value;
        }

        public Boolean VaccinationStatus
        {
            get => _vaccinated;
            set => _vaccinated = value;
        }
        public String Name;
        public String Surname;
        public String Midname;
        public String Birthday;
        private Int64 _weeksToRecover;
        private Boolean _vaccinated;
        private Int64 _weeksToBeVaccinated;
        
        public readonly Double Salary;

    }
}