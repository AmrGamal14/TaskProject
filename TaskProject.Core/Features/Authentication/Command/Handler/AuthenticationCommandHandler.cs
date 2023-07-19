using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Authentication.Command.Models;
using TaskProject.Data.Entities.Identity;
using TaskProject.Data.Helper;
using TaskProject.Service.Abstracts;

namespace TaskProject.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand,Response<JwtAuthResult>>
                                                                 , IRequestHandler<RefreshTokenCommand,Response<JwtAuthResult>>
    {
        #region Fields
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(UserManager<User> userManger, SignInManager<User> signInManager, IAuthenticationService authenticationService)
        {
            _userManger = userManger;
            _signInManager=signInManager;
            _authenticationService = authenticationService;

        }


        #endregion

        #region Handle Functions
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
          //Check if user is exist or not
           var user =await  _userManger.FindByNameAsync(request.UserName);
            //Return the User NotFound
            if (user == null) return BadRequest<JwtAuthResult>(); 
            //try to sign in 
            var SigninResult = _signInManager.CheckPasswordSignInAsync(user, request.Password,false);
            //if Field Return Password is wrong
            if (SigninResult.Result == SignInResult.Failed) return BadRequest<JwtAuthResult>();
            // Generate Token 
            var Result =await _authenticationService.GetJWTToken(user);
            return success(Result);


        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await  _authenticationService.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return success(result);
        }
        #endregion
    }
}
