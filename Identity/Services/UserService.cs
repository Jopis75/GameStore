using Application.Dtos.General;
using Application.Dtos.Identity;
using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<UserService> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UserDto>> ReadAllAsync()
        {
            try
            {
                _logger.LogInformation("Begin UserService.ReadAllAsync.");

                var applicationUsers = await _userManager.GetUsersInRoleAsync(Roles.User.ToString());

                var userDtos = applicationUsers
                    .Select(_mapper.Map<UserDto>)
                    .ToArray();

                var httpResponseDto = new HttpResponseDto<UserDto>(userDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done UserService.ReadAllAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UserDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UserService.ReadAllAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<UserDto>> ReadByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation("Begin UserService.ReadByIdAsync {@Id}.", id);

                if (String.IsNullOrEmpty(id))
                {
                    var httpResponseDto1 = new HttpResponseDto<UserDto>(new ArgumentNullException(nameof(id)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var applicationUser = await _userManager.FindByIdAsync(id);

                if (applicationUser == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UserDto>(new NotFoundException($"Could not find user {id}.", id).Message, StatusCodes.Status404NotFound);
                    _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var userDto = _mapper.Map<UserDto>(applicationUser);

                var httpResponseDto = new HttpResponseDto<UserDto>(userDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UserDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
