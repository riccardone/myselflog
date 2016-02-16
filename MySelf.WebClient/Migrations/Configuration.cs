using System.Web.Security;
using MySelf.WebClient.Models;
using WebMatrix.WebData;

//namespace MySelf.WebClient.Migrations
//{
//    using System;
//    using System.Data.Entity;
//    using System.Data.Entity.Migrations;
//    using System.Linq;

//    internal sealed class Configuration : DbMigrationsConfiguration<MySelf.WebClient.Models.MySelfClientDb>
//    {
//        public Configuration()
//        {
//            AutomaticMigrationsEnabled = false;
//        }

//        protected override void Seed(MySelf.WebClient.Models.MySelfClientDb context)
//        {
//            //  This method will be called after migrating to the latest version.

//            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//            //  to avoid creating duplicate seed data. E.g.
//            //
//            //    context.People.AddOrUpdate(
//            //      p => p.FullName,
//            //      new Person { FullName = "Andrew Peters" },
//            //      new Person { FullName = "Brice Lambson" },
//            //      new Person { FullName = "Rowan Miller" }
//            //    );
//            //

//            ContextConfig.InitAuthentication();
//            SeedAuthentication();
//        }

//        private static void SeedAuthentication()
//        {
//            var roles = (SimpleRoleProvider)Roles.Provider;
//            var membership = (SimpleMembershipProvider)Membership.Provider;

//            const string adminUser = "user@admin.com";
//            const string adminRole = "Admin";
//            const string userRole = "User";

//            if (!roles.RoleExists(adminRole))
//            {
//                roles.CreateRole(adminRole);
//            }
//            if (!roles.RoleExists(userRole))
//            {
//                roles.CreateRole(userRole);
//            }
//            if (membership.GetUser(adminUser, false) == null)
//            {
//                membership.CreateUserAndAccount(adminUser, "Pa$$w0rd");
//            }
//            if (!roles.GetRolesForUser(adminUser).Contains(adminRole))
//            {
//                roles.AddUsersToRoles(new[] { adminUser }, new[] { adminRole });
//            }
//        }
//    }
//}
