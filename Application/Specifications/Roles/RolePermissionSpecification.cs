using Application.Dtos.RoleDtos;
using Core.Entities;

namespace Application.Specifications.Users
{
    public class RolePermissionSpecification : BaseSpecification<RolePermission>
    {
        public RolePermissionSpecification(int roleId) : base(x => x.RoleId == roleId)
        {
        }

    }
}
