using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class UpdateConsoleVideoGameRequestHandler : IRequestHandler<UpdateConsoleVideoGameRequest, HttpResponseDto<UpdateConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateConsoleVideoGameRequestDto> _validator;

        private readonly ILogger<UpdateConsoleVideoGameRequestHandler> _logger;

        public UpdateConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateConsoleVideoGameRequestDto> validator, ILogger<UpdateConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UpdateConsoleVideoGameResponseDto>> Handle(UpdateConsoleVideoGameRequest updateConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateConsoleVideoGame {@UpdateConsoleVideoGameRequest}.", updateConsoleVideoGameRequest);

                if (updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto.Id);
                _mapper.Map(updateConsoleVideoGameRequest.UpdateConsoleVideoGameRequestDto, consoleVideoGame);
                var updatedConsoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.UpdateAsync(consoleVideoGame);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(new UpdateConsoleVideoGameResponseDto
                {
                    Id = updatedConsoleVideoGame.Id,
                    UpdatedAt = updatedConsoleVideoGame.UpdatedAt,
                    UpdatedBy = updatedConsoleVideoGame.UpdatedBy
                }, StatusCodes.Status200OK);
                _logger.LogInformation("End UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
