using MediTracker.Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediTracker.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationReminder> MedicationReminders { get; set; } 
        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<DoseLog> DoseLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
