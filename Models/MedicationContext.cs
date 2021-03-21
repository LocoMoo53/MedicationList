/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Using directive for EF Core
using Microsoft.Extensions.DependencyInjection; // Using directive for dependency injection
using Microsoft.AspNetCore.Identity; // Using directive for Identity namespace
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Using directive for Identity EFC

namespace MedicationList.Models
{
    public class MedicationContext : IdentityDbContext<User> // Inherit IdentityDbContext User class
    {
        public MedicationContext(DbContextOptions<MedicationContext> options) 
            : base(options) // Context constructor
        { }

        public DbSet<DrugClass> DrugClasses { get; set; } // Generate DrugClasses database table
        public DbSet<Uom> Uoms { get; set; } // Generate UoMs database table
        public DbSet<DosageForm> DosageForms { get; set; } // Generate DosageForms database table
        public DbSet<Route> Routes { get; set; } // Generate Routes database table
        public DbSet<Medication> Medications { get; set; } // Generate Medications database table

        protected override void OnModelCreating(ModelBuilder modelBuilder) { // Configure context
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DrugClass>().HasData( // Seed Classes database
                new DrugClass { DrugClassId = 1, Name = "ACE Inhibitor" },
                new DrugClass { DrugClassId = 2, Name = "Beta Blocker" },
                new DrugClass { DrugClassId = 3, Name = "Inhaled Corticosteroid" },
                new DrugClass { DrugClassId = 4, Name = "NSAID" },
                new DrugClass { DrugClassId = 5, Name = "Statin" }
            );

            modelBuilder.Entity<Uom>().HasData( // Seed UOMs database
                new Uom { UomId = 1, Name = "mcg" },
                new Uom { UomId = 2, Name = "mg" },
                new Uom { UomId = 3, Name = "mL" },
                new Uom { UomId = 4, Name = "units" }
            );

            modelBuilder.Entity<DosageForm>().HasData( // Seed Forms database
                new DosageForm { DosageFormId = 1, Name = "Capsule" },
                new DosageForm { DosageFormId = 2, Name = "MDI" },
                new DosageForm { DosageFormId = 3, Name = "Tablet" },
                new DosageForm { DosageFormId = 4, Name = "Vial" }
            );

            modelBuilder.Entity<Route>().HasData( // Seed Routes database
                new Route { RouteId = 1, Name = "Inhalation" },
                new Route { RouteId = 2, Name = "Intramuscular" },
                new Route { RouteId = 3, Name = "Oral" },
                new Route { RouteId = 4, Name = "Subcutaneous" }
            );

            modelBuilder.Entity<Medication>().HasData( // Seed Medications database
                new Medication { MedicationId = 1, DrugClassId = 1, Name = "Lisinopril", Strength = 40, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 2, DrugClassId = 1, Name = "Ramipril", Strength = 5, UomId = 2, DosageFormId = 1, RouteId = 3 },
                new Medication { MedicationId = 3, DrugClassId = 3, Name = "Flovent", Strength = 110, UomId = 1, DosageFormId = 2, RouteId = 1 },
                new Medication { MedicationId = 4, DrugClassId = 4, Name = "Naproxen", Strength = 500, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 5, DrugClassId = 5, Name = "Atorvastatin", Strength = 40, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 6, DrugClassId = 5, Name = "Simvastatin", Strength = 40, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 7, DrugClassId = 2, Name = "Metoprolol XR", Strength = 25, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 8, DrugClassId = 2, Name = "Carvedilol", Strength = 25, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 9, DrugClassId = 2, Name = "Atenolol", Strength = 25, UomId = 2, DosageFormId = 3, RouteId = 3 },
                new Medication { MedicationId = 10, DrugClassId = 4, Name = "Meloxicam", Strength = 15, UomId = 2, DosageFormId = 3, RouteId = 3 }
            );
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>(); // Create & set variable for user manager
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); // Create & set variable for role manager

            string username = "admin"; // Create & set variable for admin username
            string password = "Pa$$w0rd"; // Create & set variable for admin password
            string roleName = "Admin"; // Create & set variable for admin role

            if (await roleManager.FindByNameAsync(roleName) == null) // Create new role if admin role doesn't exist
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(username) == null) // Create new user if admin user doesn't exist
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}