/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity; // Using directive for Identity namespace

namespace MedicationList.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; } // Create property for multiple users
        public IEnumerable<IdentityRole> Roles { get; set; } // Create property for multiple roles
    }
}