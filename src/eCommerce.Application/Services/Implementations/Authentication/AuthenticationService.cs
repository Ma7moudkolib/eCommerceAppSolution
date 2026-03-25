using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace eCommerce.Application.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private AppUser? _user;

        public AuthenticationService(UserManager<AppUser> userManager , IMapper mapper ,IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<ServiceResponse> RegisterUser(CreateUser createUser)
        {
            if (!isValidUserRegisteration(createUser))
                return new ServiceResponse(false, "User Name or Email already exists");
            var userEntity =  _mapper.Map<AppUser>(createUser);
            
            var result = await _userManager.CreateAsync(userEntity, createUser.Password!);
            if(!result.Succeeded)
                return new ServiceResponse(false,result.ToString());
               
            return new ServiceResponse { Success = true , message = "Account Created!" }; 
        }
        public async Task<LoginResponse> ValidateUser(LoginUser login)
        {
             _user = await _userManager.FindByEmailAsync(login.Email!);
            if (_user is null && await _userManager.CheckPasswordAsync(_user!, _user?.PasswordHash!))
                return new LoginResponse(massage: $"Authentication failed for user {login.Email}");
            return new LoginResponse(Success: true);

        }
        public async Task<LoginResponse> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new LoginResponse(true,massage:"Success Login",token);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user!.UserName!)
            };
            var roles = await _userManager.GetRolesAsync(_user!);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }

        private bool isValidUserRegisteration(CreateUser createUser )
        {
            return  _userManager.FindByEmailAsync(createUser.Email!) != null 
                &&  _userManager.FindByNameAsync(createUser.UserName) != null;
        }

    }
}
