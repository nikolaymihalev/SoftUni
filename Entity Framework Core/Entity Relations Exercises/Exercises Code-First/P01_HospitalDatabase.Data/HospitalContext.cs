using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Configurations;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext:DbContext
    {
        const string ConnectionString = "Server=.;Database=Hospital;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false";

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfiguration());

            modelBuilder.Entity<Visitation>().Property(v => v.Comments).HasMaxLength(250).IsUnicode(true);

            modelBuilder.Entity<Diagnose>().Property(d => d.Name).HasMaxLength(50).IsUnicode(true);
            modelBuilder.Entity<Diagnose>().Property(d => d.Comments).HasMaxLength(250).IsUnicode(true);

            modelBuilder.Entity<Medicament>().Property(m => m.Name).HasMaxLength(50).IsUnicode(true);

            modelBuilder.Entity<PatientMedicament>().HasKey(pm => new { pm.PatientId, pm.MedicamentId });
        }
    }
}
