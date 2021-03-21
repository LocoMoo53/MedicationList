/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

namespace MedicationList.Models
{
    public class Filters
    {
        public Filters(string filterstring) // create Filters object containing drug classes, dosage forms, and routes for filtering
        {
            FilterString = filterstring ?? "all-all-all";
            string[] filters = FilterString.Split('-');
            DrugClassId = filters[0];
            DosageFormId = filters[1];
            RouteId = filters[2];
        }
        public string FilterString { get; } // create property to get filter string
        public string DrugClassId { get; } // create property to get drug class ID
        public string DosageFormId { get; } // create property to get dosage form ID
        public string RouteId { get; } // create property to get route ID

        public bool HasDrugClass => DrugClassId.ToString().ToLower() != "all"; // create method for converting drug class ID to lowercase string
        public bool HasDosageForm => DosageFormId.ToString().ToLower() != "all"; // create method for converting dosage form ID to lowercase string
        public bool HasRoute => RouteId.ToString().ToLower() != "all"; // create method for converting route ID to lowercase string
    }
}