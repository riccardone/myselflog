using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.ModelConfigurations
{
    public class FoodConfiguration : EntityTypeConfiguration<Food>
    {
        public FoodConfiguration()
        {
            HasKey(k => k.Id);
            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}
