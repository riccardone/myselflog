using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.ModelConfigurations
{
    public class FriendActivityConfiguration : EntityTypeConfiguration<FriendActivity>
    {
        public FriendActivityConfiguration()
        {
            HasKey(k => k.Id);
            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(p => p.Description)
                .IsRequired();
        }
    }
}
