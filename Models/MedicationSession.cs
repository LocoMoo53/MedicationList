/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Collections.Generic;
using Microsoft.AspNetCore.Http; // using directive for HTTP

namespace MedicationList.Models
{
    public class MedicationSession
    {
        private const string MedicationsKey = "mymedications"; // Create & set constant variable for medications key
        private const string CountKey = "medicationcount"; // Create & set constant variable for medication count

        private ISession session { get; set; } // Create property for session
        public MedicationSession(ISession session) { // Create method for current medication session
            this.session = session;
        }

        public void SetMyMedications(List<Medication> medications) { // Create method for setting medication refills
            session.SetObject(MedicationsKey, medications);
            session.SetInt32(CountKey, medications.Count);
        }
        public List<Medication> GetMyMedications() =>
            session.GetObject<List<Medication>>(MedicationsKey) ?? new List<Medication>(); // Create method for getting medication refills
        public int? GetMyMedicationCount() => session.GetInt32(CountKey); // Create method for getting medication refill count

        public void RemoveMyMedications() { // Create method for deleting medication refills
            session.Remove(MedicationsKey);
            session.Remove(CountKey);
        }
    }
}