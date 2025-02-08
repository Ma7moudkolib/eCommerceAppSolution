using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Application.Validations;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.WebSockets;

namespace eCommerce.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(IUserManagement userManagement
        , IRoleManagement roleManagement
        ,ITokenManagement tokenManagement , 
        IAppLogger<AuthenticationService> logger , IMapper mapper ,IValidator<CreateUser> createUserValidation,
        IValidator<LoginUser> loginUserValidation,
        IValidationService validation  ) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser createUser)
        {
            var validationResult = await validation.ValidateAsync(createUser, createUserValidation);
            if (!validationResult.Success)
            {
                return validationResult;
            }
            var mappedModel =  mapper.Map<AppUser>(createUser);
            mappedModel.UserName = createUser.Email;
            mappedModel.PasswordHash = createUser.Password;

            var result = await userManagement.CreateUser(mappedModel);
            if(!result)
            {
                return new ServiceResponse { message = "Email Address maigh be is already in use or unknown error occurred."};
            }
            var _user = await userManagement.GetUserByEmail(createUser.Email);
            var _users = await userManagement.GetAllUsers();
            bool assignResult = await roleManagement.AddUserToRole(_user!, _users!.Count() > 1 ? "User" : "Admin");
            if (!assignResult)
            {
                //remove user 
                int removeUserResult = await userManagement.RemoveUserByEmail(createUser.Email);
                if(removeUserResult <= 0)
                {
                    // error occuered while rolling back changes
                    //then log the error 
                    logger.LogError(new Exception
                        ($"User with Email as {createUser.Email} falil to be remove as a result of role assigning issue."),
                        "User could not be assigned Role.");
                    return new ServiceResponse { message = "Error Occured in Creating Acount" };
                        
                }
            }
            return new ServiceResponse { Success = true , message = "Account Created!" };
            //Verify Email 

        }

        public async Task<LoginResponse> LoginUser(LoginUser login)
        {
            var _validationResult = await validation.ValidateAsync( login, loginUserValidation);
            if(!_validationResult.Success)
                return new LoginResponse(massage: _validationResult.message);
            var mappedModel = mapper.Map<AppUser>(login);
            mappedModel.PasswordHash = login.Password;
            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult)
                return new LoginResponse(massage: "Email not found or invalid credentials");

            var _user = await userManagement.GetUserByEmail(login.Email);
            var claims = await userManagement.GetUserClaims(_user.Email!);

            string jwtToken =  tokenManagement.GenerateToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();

            int saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            return saveTokenResult <= 0 ? new LoginResponse(massage: "Internal error occurred while authentiacatint.") :
                new LoginResponse(Success: true, Token: jwtToken,refreshToken: refreshToken);
        }

        public async Task<LoginResponse> RevivToken(string refreshToken)
        {
          var validateTokenResult = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateTokenResult)
                return new LoginResponse(massage: "Invalid Token");

            string UserId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            var user = await userManagement.GetUserById(UserId);

            var claims = await userManagement.GetUserClaims(user!.Email!);
            var newJwtToken = tokenManagement.GenerateToken(claims);
            var newRefreshToken = tokenManagement.GetRefreshToken();

            await tokenManagement.UpdateRefreshToken( newRefreshToken);
            return new LoginResponse(Success: true, Token: newJwtToken, refreshToken: refreshToken);
        }
    }
}
