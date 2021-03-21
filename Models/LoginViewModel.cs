/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Using directive for data annotations

namespace MedicationList.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")] // Make username required
        [StringLength(255)] // Set maximum username length to 255
        public string Username { get; set; } // Create property for username

        [Required(ErrorMessage = "Please enter a password.")] // Make password required
        [StringLength(255)] // Set maximum password length to 255
        public string Password { get; set; } // Create property for password

        public string ReturnUrl { get; set; } // Create property for return URL
        
        public bool RememberMe { get; set; } // Create property for remember me
    }
}