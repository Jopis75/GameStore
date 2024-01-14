using Application.Dtos.Common;
using Application.Dtos.Identity;
using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadUserByUserIdRequestDto> _validator;

        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, IValidator<ReadUserByUserIdRequestDto> validator, ILogger<UserService> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadUserResponseDto>> ReadAllAsync(ReadUserAllRequestDto readUserAllRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin UserService.ReadAllAsync {@ReadUserAllRequestDto}.", readUserAllRequestDto);

                var applicationUsers = await _userManager.GetUsersInRoleAsync(Roles.User.ToString());

                var readUserResponseDtos = applicationUsers
                    .Select(_mapper.Map<ReadUserResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadUserResponseDto>(readUserResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done UserService.ReadAllAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadUserResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UserService.ReadAllAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<ReadUserResponseDto>> ReadByIdAsync(ReadUserByUserIdRequestDto readUserByUserIdRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin UserService.ReadByIdAsync {@ReadUserByUserIdRequestDto}.", readUserByUserIdRequestDto);

                if (readUserByUserIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadUserResponseDto>(new ArgumentNullException(nameof(readUserByUserIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readUserByUserIdRequestDto);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadUserResponseDto>(new FluentValidation.ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var applicationUser = await _userManager.FindByIdAsync(readUserByUserIdRequestDto.UserId);

                if (applicationUser == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadUserResponseDto>(new NotFoundException($"Could not find user {readUserByUserIdRequestDto.UserId}.", readUserByUserIdRequestDto.UserId).Message, StatusCodes.Status404NotFound);
                    _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var readUserResponseDto = _mapper.Map<ReadUserResponseDto>(applicationUser);

                var httpResponseDto = new HttpResponseDto<ReadUserResponseDto>(readUserResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadUserResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UserService.ReadByIdAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
