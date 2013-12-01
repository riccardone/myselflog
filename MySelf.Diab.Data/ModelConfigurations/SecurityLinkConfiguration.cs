using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.ModelConfigurations
{
    public class SecurityLinkConfiguration : EntityTypeConfiguration<SecurityLink>
    {
        public SecurityLinkConfiguration()
        {
            HasKey(k => k.Id);
            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(p => p.Link)
                .IsRequired().HasMaxLength(255);
        }
    }
}
