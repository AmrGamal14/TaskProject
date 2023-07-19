using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Data.Entities.Identity;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Infrastructure.Data;
using TaskProject.Infrastructure.InfrastructureBases;

namespace TaskProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>,IRefreshTokenRepository
    {
        #region Fields
        private readonly DbSet<UserRefreshToken> userRefreshToken;
        #endregion
        #region Constructors
        public RefreshTokenRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            userRefreshToken=dBContext.Set<UserRefreshToken>();
        }
        #endregion
    }
}
