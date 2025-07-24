using Application.Dtos.RoleDtos;
using Application.Dtos.UserDtos;
using Core.Entities;

namespace Application.Specifications.Roles
{
    public class RoleSpecification : BaseSpecification<Role>
    {
        public RoleSpecification(RoleFilterParams filterParams)
            : base(x =>
                (string.IsNullOrEmpty(filterParams.Search) ||
                 x.Name.Contains(filterParams.Search) ||
                 x.Id.ToString() == filterParams.Search)
            )
        {
            ApplyPaging(filterParams.PageSize * (filterParams.PageIndex - 1), filterParams.PageSize);

            if (!string.IsNullOrEmpty(filterParams.Sort))
            {
                switch (filterParams.Sort)
                {
                    case "NameAsc": AddOrderBy(u => u.Name); break;
                    case "NameDesc": AddOrderByDescending(u => u.Name); break;
                    case "CreatedDateAsc": AddOrderBy(u => u.CreatedDate); break;
                    case "CreatedDateDesc": AddOrderByDescending(u => u.CreatedDate); break;
                    default: AddOrderBy(u => u.Id); break;
                }
            }

        }
        public RoleSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.RolePermissions);
        }

    }
}
