﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Features.UserCQRS.Query.Result
{
    public class GetUserListResult
    {
        public string FullName { get; set; }
        public string Email { get; set; } 
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
