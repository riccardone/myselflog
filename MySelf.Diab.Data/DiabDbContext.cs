using System.Data.Entity;
using MySelf.Diab.Data.ModelConfigurations;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data
{
    public class DiabDbContext : DbContext 
    {
        public DbSet<GlucoseLevel> GlucoseLevels { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<LogProfile> LogProfiles { get; set; }
        public DbSet<FriendActivity> FriendActivities { get; set; }
        public DbSet<SecurityLink> SecurityLinks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GlucoseLevelConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new LogProfileConfiguration());
            modelBuilder.Configurations.Add(new FriendActivityConfiguration());
            modelBuilder.Configurations.Add(new SecurityLinkConfiguration());
        }
    }
}
