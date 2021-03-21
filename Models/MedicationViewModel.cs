/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Collections.Generic;

namespace MedicationList.Models
{
    public class MedicationViewModel
    {
        public MedicationViewModel() // method for specific medication
        {
            CurrentMedication = new Medication();
        }

        public Filters Filters { get; set; } // create property for filters
        public List<DrugClass> DrugClasses { get; set; } // create property for list of drug classes
        public List<Uom> Uoms { get; set; } // create property for list of UoMs
        public List<DosageForm> DosageForms { get; set; } // create property for list of dosage forms
        public List<Route> Routes { get; set; } // create property for list of routes
        public List<Medication> Medications { get; set; } // create property for list of medications
        public Medication CurrentMedication { get; set; } // create proeprty for specific medication
    }
}