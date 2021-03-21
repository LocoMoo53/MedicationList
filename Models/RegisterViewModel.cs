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
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")] // Make username required
        [StringLength(255)] // Set username maximum length to 255 characters
        public string Username { get; set; } // Create property for username

        [Required(ErrorMessage = "Please enter a password.")] // Make password required
        [DataType(DataType.Password)] // Specify password data type
        [Compare("ConfirmPassword")] // Compare password value to confirm password value
        public string Password { get; set; } // Create property for password

        [Required(ErrorMessage = "Please confirm your password")] // Make confirm password required
        [DataType(DataType.Password)] // Specify password data type
        [Display(Name = "ConfirmPassword")] // Show comparison result
        public string ConfirmPassword { get; set; } // Create property for confirm password
    }
}