using System.Collections.Generic;
using BulletRay.Roles.Dto;
using BulletRay.Users.Dto;

namespace BulletRay.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
