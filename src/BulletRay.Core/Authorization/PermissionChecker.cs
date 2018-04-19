using Abp.Authorization;
using BulletRay.Authorization.Roles;
using BulletRay.Authorization.Users;

namespace BulletRay.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
