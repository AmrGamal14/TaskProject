using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.UserCQRS.Command.Models;
using TaskProject.Data.Entities.Identity;

namespace TaskProject.Core.Features.UserCQRS.Command.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
                                                     , IRequestHandler<UpdateUserCommand, Response<string>>
                                                     , IRequestHandler<DeleteUserCommand, Response<string>>
                                                     , IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors 
        public UserCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;

        }
        #endregion

        #region Handle Functions
        public async  Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email is Exist
            var useremail = await _userManager.FindByEmailAsync(request.Email);
            //Email is Exist 
            if (useremail != null) return BadRequest<string>("email is exist ");
            //if UserName is Exist 
            var username = await _userManager.FindByNameAsync(request.UserName);
            //UserName is Exist 
            if (username != null) return BadRequest<string>("username is already exist ");
            //Mapping 
            var identityuser = _mapper.Map<User>(request);
            //Create
            var createruslt = await _userManager.CreateAsync(identityuser,request.Password); 
            //Field 
            if (!createruslt.Succeeded) return BadRequest<string>("Field to AddUser ");
            await _userManager.AddToRoleAsync(identityuser, "User");
            //Create 
            //Success
            return Created("");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>();
            var newUser = _mapper.Map(request, oldUser);
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded) return BadRequest<string>();
            return success("");



        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest<string>();
            return success("");

        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded) return BadRequest<string>();
            return success(""); 
        }

        #endregion

    }
}
