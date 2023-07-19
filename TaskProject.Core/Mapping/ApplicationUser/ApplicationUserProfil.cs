using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfil : Profile
    {
        public ApplicationUserProfil()
        {
            AddUserMapping();
             GetListUser();
            UpdateUserMapping();
        }
        
    }
}
