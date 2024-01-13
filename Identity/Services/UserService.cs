using Application.Dtos.Identity;
using Application.Interfaces.Identity;
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

        public async Task<IEnumerable<UserDto>> ReadAllAsync()
        {
            var applicationUsers = await _userManager.GetUsersInRoleAsync("User");

            return applicationUsers
                .Select(applicationUser => new UserDto // ToDo: Use AutoMapper.
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName
                })
                .ToList();
        }

        public async Task<UserDto> ReadByIdAsync(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);

            if (applicationUser == null)
            {
                return new UserDto();
            }

            return new UserDto // ToDo: Use AutoMapper.
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName
            };
        }
    }
}
