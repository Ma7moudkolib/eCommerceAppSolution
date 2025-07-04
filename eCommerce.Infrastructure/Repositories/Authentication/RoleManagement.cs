﻿using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRole(AppUser user, string roleName)
        {
           return (await userManager.AddToRoleAsync(user, roleName)).Succeeded;
        }

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            var role = await userManager.GetRolesAsync(user!);
            return role.FirstOrDefault();
        }
    }
}
