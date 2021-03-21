/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity; // Using directive for Identity namespace
using System.ComponentModel.DataAnnotations.Schema; // Using directive for data annotations schema

namespace MedicationList.Models
{
    public class User : IdentityUser // User class that inherits IdentityUser class
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; } // Create property for list of role names
    }
}