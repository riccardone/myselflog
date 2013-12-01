using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.ModelConfigurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            ToTable("People");
            HasKey(k => k.Id);
            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(p => p.Email)
                .HasMaxLength(255)
                .IsRequired();

            Property(p => p.Country)
                .HasMaxLength(255);

            Property(p => p.FirstName)
                .HasMaxLength(255);

            Property(p => p.LastName)
                .HasMaxLength(255);

            Property(p => p.PostalCode)
                .HasMaxLength(50);
        }
    }
}
