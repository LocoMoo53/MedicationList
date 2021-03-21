/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http; // add using directive for HTTP

namespace MedicationList.Models
{
    public class MedicationCookies
    {
        private const string MedicationsKey = "mymedications"; // Add & set constant variable for medications key
        private const string Delimiter = "-"; // Add & set constant variable for cookie delimiter

        private IRequestCookieCollection requestCookies { get; set; } // Create property for cookie requests
        private IResponseCookies responseCookies { get; set; } // Create property for cookie responses
        public MedicationCookies(IRequestCookieCollection cookies) // Create method for cookie requests
        {
            requestCookies = cookies;
        }
        public MedicationCookies(IResponseCookies cookies) // Create method for cookie responses
        {
            responseCookies = cookies;
        }

        public void SetMyMedicationIds(List<Medication> mymedications) // Create method for setting medication IDs
        {
            List<string> ids = mymedications.Select(m => m.MedicationId.ToString()).ToList();
            string idsString = String.Join(Delimiter, ids);
            RemoveMyMedicationIds();
            responseCookies.Append(MedicationsKey, idsString);
        }

        public string[] GetMyMedicationIds() // Create method for getting medication IDs
        {
            string cookie = requestCookies[MedicationsKey];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { };
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMyMedicationIds() // Create method for removing medication IDs
        {
            responseCookies.Delete(MedicationsKey);
        }
    }
}