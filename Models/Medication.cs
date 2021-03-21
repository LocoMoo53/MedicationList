/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.ComponentModel.DataAnnotations; // Using directive for data annotations

namespace MedicationList.Models
{
    public class Medication
    {
        // EF Core will configure database to auto-generate medication ID
        public int MedicationId { get; set; } // Create property for medication ID

        [Required(ErrorMessage = "Please select a drug class.")] // Make medication drug class required
        public int? DrugClassId { get; set; } // Create property for medication drug class ID foreign key
        public DrugClass DrugClass { get; set; } // Create property for medication drug class

        [Required(ErrorMessage = "Please enter a name.")] // Make medication name required
        public string Name { get; set; } // Create property for medication name

        [Required(ErrorMessage = "Please enter a strength.")] // Make medication strength required
        [Range(1, 1000, ErrorMessage = "Strength must be between 1 and 1000 (inclusive).")] // Set range for medication strength
        public int? Strength { get; set; } // Create property for medication strength

        [Required(ErrorMessage = "Please select a unit of measure.")] // Make medication UoM required
        public int? UomId { get; set; } // Create property for medication UoM ID foreign key
        public Uom Uom { get; set; } // Create property for medication UoM

        [Required(ErrorMessage = "Please select a dosage form.")] // Make medication dosage form required
        public int? DosageFormId { get; set; } // Create property for medication dosage form ID foreign key
        public DosageForm DosageForm { get; set; } // Create property for medication dosage form

        [Required(ErrorMessage = "Please select a route.")] // Make medication route required
        public int? RouteId { get; set; } // Create property for medication route ID foreign key
        public Route Route { get; set; } // Create property for medication route

        public string Slug // Create slug for URL
        {
            get
            {
                if (Name == null)
                    return "";
                else
                    return Name.Replace(' ', '-');
            }
        }
    }
}