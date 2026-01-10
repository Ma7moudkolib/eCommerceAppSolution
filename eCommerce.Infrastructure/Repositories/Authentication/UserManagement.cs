using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class UserManagement(UserManager<AppUser> userManager ,
        IRoleManagement roleManagement ,AppDbContext context) : IUserManagement
    {
        public async Task<bool> CreateUser(AppUser user)
        {
           var _user = await userManager.FindByEmailAsync(user.Email!);
            if (_user != null) { return false; }

            return (await userManager.CreateAsync(user! , user!.PasswordHash!)).Succeeded;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user!;
        }
       

        public async Task<AppUser> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user!;
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var _user = await GetUserByEmail(email);
            string? roleName = await roleManagement.GetUserRole(_user.Email!);
            List<Claim> claims = [
                new Claim(ClaimTypes.NameIdentifier,_user.Id),
                new Claim(ClaimTypes.Email,_user.Email!),
                new Claim(ClaimTypes.Role , roleName!)
                ];
            return claims;
        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user == null) { return false; }
            var roleName = await roleManagement.GetUserRole(user.Email!);
            if(roleName == null) { return false; }
            var ans = await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
            return ans;
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
           var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            context.Users.Remove(user!);
            return await context.SaveChangesAsync();


        }
       
    }
}
