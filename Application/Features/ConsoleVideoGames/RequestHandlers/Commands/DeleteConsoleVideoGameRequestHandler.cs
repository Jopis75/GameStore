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
    public class DeleteConsoleVideoGameRequestHandler : IRequestHandler<DeleteConsoleVideoGameRequest, HttpResponseDto<DeleteConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteConsoleVideoGameRequestDto> _validator;

        private readonly ILogger<DeleteConsoleVideoGameRequestHandler> _logger;

        public DeleteConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteConsoleVideoGameRequestDto> validator, ILogger<DeleteConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DeleteConsoleVideoGameResponseDto>> Handle(DeleteConsoleVideoGameRequest deleteConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteConsoleVideoGame {@DeleteConsoleVideoGameRequest}.", deleteConsoleVideoGameRequest);

                if (deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(deleteConsoleVideoGameRequest.DeleteConsoleVideoGameRequestDto.Id);
                var deletedConsoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.DeleteAsync(consoleVideoGame);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(new DeleteConsoleVideoGameResponseDto
                {
                    Id = deletedConsoleVideoGame.Id,
                    DeletedAt = deletedConsoleVideoGame.DeletedAt,
                    DeletedBy = deletedConsoleVideoGame.DeletedBy
                }, StatusCodes.Status200OK);
                _logger.LogInformation("End DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
