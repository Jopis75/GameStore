using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IEnumerable<User>> ReadAllAsync()
        {
            var applicationUsers = await _userManager.GetUsersInRoleAsync("User");

            return applicationUsers
                .Select(applicationUser => new User // ToDo: Use AutoMapper.
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName
                })
                .ToList();
        }

        public async Task<User> ReadByIdAsync(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);

            if (applicationUser == null)
            {
                return new User();
            }

            return new User // ToDo: Use AutoMapper.
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName
            };
        }
    }
}
