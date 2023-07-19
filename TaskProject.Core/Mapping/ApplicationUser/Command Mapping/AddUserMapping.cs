using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.UserCQRS.Command.Models;
using TaskProject.Data.Entities.Identity;

namespace TaskProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfil
    { public void AddUserMapping ()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}
