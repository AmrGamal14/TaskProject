﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities.Identity;
using TaskProject.Data.Helper;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Service.Abstracts;

namespace TaskProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
       
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            
            _userManager = userManager;

        }


        #endregion

        #region Handle Functions 
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken)=GenerateJwtToken(user);
            var refreshToken = new RefreshToken
            {
                ExpireAt= DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName=user.UserName,
                TokenString=GenerateRefreshToken()

            };
            
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime=DateTime.Now,
                ExpiryDate =DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed=true,
                IsRevoked=false,
                JwtId=jwtToken.Id,
                RefreshToken=refreshToken.TokenString,
                Token=accessToken,
                UserId=user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            var response = new JwtAuthResult();
            response.refreshToken=refreshToken;
            response.AccessToken=accessToken;
            response.Roles = (List<string>)await _userManager.GetRolesAsync(user);
            return response;
        }
        private (JwtSecurityToken,string) GenerateJwtToken(User user)
            
        {
            var cliams = new List<Claim>() 
            {
                new Claim (nameof(UserClaimModel.UserName),user.UserName),
                new Claim (nameof(UserClaimModel.Email),user.Email),
                new Claim (nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),
                new Claim (nameof(UserClaimModel.Id),user.Id.ToString()),
            };
            var jwtToken = new JwtSecurityToken(_jwtSettings.Issuer,
                                             _jwtSettings.Audience,
                                             cliams,
                                            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken,accessToken);
        }

        private string GenerateRefreshToken()
        { var randomNumber=new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);


        }
        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            var jwtToken =  ReadJwtToken(accessToken);
            if (jwtToken==null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invaid token");
            }
            if (jwtToken.ValidTo>DateTime.UtcNow)
            {
                throw new SecurityTokenException("token is not expired");
            }
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type==nameof(UserClaimModel.Id)).Value;

            var userRefreshToken = _refreshTokenRepository.GetTableNoTracking().FirstOrDefault(x => x.Token==accessToken && x.RefreshToken==refreshToken&&x.UserId==int.Parse(userId));

            if (userRefreshToken==null)
            {
                throw new SecurityTokenException("Refresh Token Is Not Found");
            }
            if (userRefreshToken.ExpiryDate<DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked=true;
                userRefreshToken.IsUsed=false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);

                throw new SecurityTokenException("Refresh Token Is Expired");
            }
            var user = await _userManager.FindByIdAsync (userId);
            if (user==null)
            {
                throw new SecurityTokenException("User Is Not Found");
            }
            var (jwtSecurityToken,newToken) = GenerateJwtToken(user);
            userRefreshToken.Token=newToken;
            await _refreshTokenRepository.UpdateAsync(userRefreshToken);
            var response = new JwtAuthResult();
            response.AccessToken=newToken;
              
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName= jwtToken.Claims.FirstOrDefault(x => x.Type==nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString=refreshToken;
            refreshTokenResult.ExpireAt=userRefreshToken.ExpiryDate;
            response.refreshToken=refreshTokenResult;
            return response;
        }
          
         
        private JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers=new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey=_jwtSettings.ValidateIssureSigningKey,
                IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience=_jwtSettings.Audience,
                ValidateAudience=_jwtSettings.ValidateAudience,
                ValidateLifetime=_jwtSettings.ValidateLifeTime,
            };
            try
            { 
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validateToken);
            
            if (validator == null)
                {
                    throw new SecurityTokenException("InvalidToken");
                }
                return "NotExpired";

            }
            catch(Exception ex)
            {
                return ex.Message;

            }
        }
        #endregion
    }
}
