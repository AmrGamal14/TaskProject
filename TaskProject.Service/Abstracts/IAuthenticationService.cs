using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities.Identity;
using TaskProject.Data.Helper;

namespace TaskProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtAuthResult> GetRefreshToken(string accessToken , string refreshkToen);
        public Task<string> ValidateToken(string accessToken);
    }
}
