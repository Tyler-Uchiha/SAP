using SAP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SAP.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SAP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<NewSuspect> Suspects_Table { get; set; }
        public DbSet<AddCriminalRec> CriminalRecords_Table { get; set; }  
        public DbSet<ListofOffences> Offences_Table { get; set; }
        public DbSet<Managers> Managers_Table { get; set; }
        public DbSet<Cases> Cases_Table { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<NewSuspect>().HasData
                (
                    new NewSuspect { Id = 1, SuspectIdNum = "0303016812181", First_Name = "Andrew", Last_Name = "Dusk", Height = 177, Eye_Color = "Green", Sex = "Male"},
                    new NewSuspect { Id = 2, SuspectIdNum = "0303011180187", First_Name = "Amanda", Last_Name = "White", Height = 158, Eye_Color = "Brown", Sex = "Female"}
                );

            modelBuilder.Entity<AddCriminalRec>().HasData
            (
                    new AddCriminalRec { Id = 1, CriminalRecordId = "C-1111-20241015-9999", CriminalIdNum = "1111111231234", OffenceCommitted = "Drugs" , Sentence = 3, IssuedAt = "Sandton" , IssuedBy = "Kelvin Moores", IssueDate = "2024-10-10", Case_Status = "Not Started"}
            );

            modelBuilder.Entity<Managers>().HasData
            (
                    new Managers { Id = 1, First_Name = "Johnny", Last_Name= "Bravo", CaseCount = 3, ManagerId  = "M-1"},
                    new Managers { Id = 2, First_Name = "Jill", Last_Name = "Scott", CaseCount = 0, ManagerId = "M-2" },
                    new Managers { Id = 3, First_Name = "Laura", Last_Name = "Croft", CaseCount = 7, ManagerId = "M-3" },
                    new Managers { Id = 4, First_Name = "Angel", Last_Name = "Jolls", CaseCount = 3, ManagerId = "M-4" },
                    new Managers { Id = 5, First_Name = "Tom", Last_Name = "Crew", CaseCount = 2, ManagerId = "M-5" }
            );

            modelBuilder.Entity<Cases>().HasData
            (
                    new Cases { Id = 1, CriminalRecordId = "C-1111-20241015-9999", CriminalIdNum = "1111111231234", OffenceCommitted = "Drugs", Sentence = 3, IssuedAt = "Sandton", IssuedBy = "Kelvin Moores", IssueDate = "2024-10-10", IsAssigned  = false, AssignedTo  = "None", ManagerId  = "None", CaseStatus = "Not Started"}      
            );

            modelBuilder.Entity<ListofOffences>().HasData
                (
                    new ListofOffences { Id = 1, Offences = "Assault", OffenceCode = "A1" },
                    new ListofOffences { Id = 2, Offences = "Burglary", OffenceCode = "A2" },
                    new ListofOffences { Id = 3, Offences = "Drugs", OffenceCode = "A3" },
                    new ListofOffences { Id = 4, Offences = "Hijacking", OffenceCode = "A4" },
                    new ListofOffences { Id = 5, Offences = "Murder", OffenceCode = "A5" },
                    new ListofOffences { Id = 6, Offences = "Offences of Dishonesty", OffenceCode = "A6" },
                    new ListofOffences { Id = 7, Offences = "Other offences", OffenceCode = "A7" },
                    new ListofOffences { Id = 8, Offences = "Public drinking", OffenceCode = "A8" },
                    new ListofOffences { Id = 9, Offences = "Rape", OffenceCode = "A9" },
                    new ListofOffences { Id = 10, Offences = "Robbery", OffenceCode = "A10" },
                    new ListofOffences { Id = 11, Offences = "Sexual Harrassment", OffenceCode = "A11" },
                    new ListofOffences { Id = 12, Offences = "Sexual Offences", OffenceCode = "A12" },
                    new ListofOffences { Id = 13, Offences = "Violence" , OffenceCode = "A13" }
               );
        }
    }
}
