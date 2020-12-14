using System;

namespace VirusModel
{
    public class Citizen
    {
        public Citizen(String name = null, String surname = null, String midname = null, String birthday = null, 
            Double salary = -1)
        {
            Name = name;
            Surname = surname;
            Midname = midname;
            Birthday = birthday;
            if (salary == -1)
            {
                Salary = ModelConstants.DefaultSalary;
            }
            else
            {
                Salary = salary;
            }
            _weeksToBeVaccinated = -1;
            _weeksToRecover = 0;
            Random rand = new Random();
        }

        public void BecomeInfected()
        {
            if (VaccinationStatus == true)
                return;
            
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
                _weeksToRecover = -ModelConstants.VaccinationDuration;
            }
            
            
        }

        private void UpdateInfectionStatus()
        {
            if (_weeksToRecover > 0)
            {
                _weeksToRecover--;
                if (_weeksToRecover == 0)
                {
                    _weeksToRecover = -ModelConstants.ImmunityWeeksDuration;
                }
            }
            else if (_weeksToRecover < 0)
            {
                _weeksToRecover++;
                if (_weeksToRecover == 0)
                {
                    VaccinationStatus = false;
                    WeeksToBeVaccinated = -1;
                }
            }
        }
        public Int64 WeeksToRecover
        {
            get => _weeksToRecover;
            set => _weeksToRecover = value;
        }
        
        public Int64 WeeksToBeVaccinated
        {
            get => _weeksToBeVaccinated;
            set => _weeksToBeVaccinated = value;
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
        public readonly Double Salary;
        private Int64 _weeksToRecover;
        private Boolean _vaccinated;
        private Int64 _weeksToBeVaccinated;
        private Boolean _isolated;



    }
}