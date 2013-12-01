using System.Data.Entity;

namespace MySelf.Diab.Data
{
    public class DiabDbInitializer : CreateDatabaseIfNotExists<DiabDbContext>
    {
        protected override void Seed(DiabDbContext context)
        {
            // DB constraints
            //context.Database.ExecuteSqlCommand("alter table TaskItems add constraint TaskItems_Name unique (CompanyId, Name)");
        }
    }
}
