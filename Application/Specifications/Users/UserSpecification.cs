using Application.Dtos.UserDtos;
using Core.Entities;
using System.Linq.Expressions;

namespace Application.Specifications.Users
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(UserFilterParams filterParams)
            : base(x =>
                (string.IsNullOrEmpty(filterParams.Search) ||
                 x.Name.Contains(filterParams.Search) ||
                 x.Email.Contains(filterParams.Search) ||
                 x.Mobile.Contains(filterParams.Search)) &&
                (string.IsNullOrEmpty(filterParams.Role) ||
                 x.Role.Name == filterParams.Role) &&
                (string.IsNullOrEmpty(filterParams.Status) ||
                 x.Status.ToString() == filterParams.Status) &&
                (!filterParams.StartDate.HasValue ||
                 x.JoiningDate >= filterParams.StartDate) &&
                (!filterParams.EndDate.HasValue ||
                 x.JoiningDate <= filterParams.EndDate)
            )
        {
            AddInclude(x => x.Role);
            ApplyPaging(filterParams.PageSize * (filterParams.PageIndex - 1), filterParams.PageSize);

            if (!string.IsNullOrEmpty(filterParams.Sort))
            {
                switch (filterParams.Sort)
                {
                    case "nameAsc": AddOrderBy(u => u.Name); break;
                    case "nameDesc": AddOrderByDescending(u => u.Name); break;
                    case "dateAsc": AddOrderBy(u => u.JoiningDate); break;
                    case "dateDesc": AddOrderByDescending(u => u.JoiningDate); break;
                    default: AddOrderByDescending(u => u.JoiningDate); break;
                }
            }
            else
            {
                AddOrderByDescending(u => u.JoiningDate);
            }

        }
        public UserSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Role);
        }

        //public UserSpecification(Expression<Func<User, bool>> criteria)
        //: base(criteria)
        //{
        //    AddInclude(x => x.Role);
        //    AddInclude(x => x.Role.RolePermissions);
        //    AddInclude("Role.RolePermissions.Permission");
        //}
    }
}
