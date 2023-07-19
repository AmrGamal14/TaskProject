using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.UserCQRS.Query.Result;
using TaskProject.Data.Entities.Identity;

namespace TaskProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfil
    {
        public void GetListUser()
        {
            CreateMap<User, GetUserListResult>();
        }
    }
}
