using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.ModelConfigurations
{
    public class GlucoseLevelConfiguration : EntityTypeConfiguration<GlucoseLevel>
    {
        public GlucoseLevelConfiguration()
        {
            ToTable("GlucoseLevels");
            HasKey(k => k.Id);
            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            //Property(p => p.LogProfileId)
            //    .IsRequired();
            Property(p => p.GlobalId)
                .IsRequired();
        }
    }
}
