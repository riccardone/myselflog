using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

namespace MySelf.Diab.Data
{
    public static class ContextConfig
    {
        public static void InitAuthentication()
        {
            WebSecurity.InitializeDatabaseConnection("DiabDbContext", "People", "Id", "Email", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            const string adminUser = "user@admin.com";

            if (!roles.RoleExists(AuthConstants.AdminRole))
            {
                roles.CreateRole(AuthConstants.AdminRole);
            }
            if (!roles.RoleExists(AuthConstants.UserRole))
            {
                roles.CreateRole(AuthConstants.UserRole);
            }
            if (!roles.RoleExists(AuthConstants.FriendRole))
            {
                roles.CreateRole(AuthConstants.FriendRole);
            }
            if (membership.GetUser(adminUser, false) == null)
            {
                membership.CreateUserAndAccount(adminUser, "Pa$$w0rd");
            }
            if (!roles.GetRolesForUser(adminUser).Contains(AuthConstants.AdminRole))
            {
                roles.AddUsersToRoles(new[] { adminUser }, new[] { AuthConstants.AdminRole });
            }
        }
    }
}