using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(50).IsUnicode(true);
            builder.Property(p => p.LastName).HasMaxLength(50).IsUnicode(true);
            builder.Property(p => p.Address).HasMaxLength(250).IsUnicode(true);
            builder.Property(p => p.Email).HasMaxLength(80).IsUnicode(false);
        }
    }
}
