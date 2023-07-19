using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities.Identity;
using TaskProject.Infrastructure.InfrastructureBases;

namespace TaskProject.Infrastructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericeRepositoryAsync<UserRefreshToken>
    {
    }
}
