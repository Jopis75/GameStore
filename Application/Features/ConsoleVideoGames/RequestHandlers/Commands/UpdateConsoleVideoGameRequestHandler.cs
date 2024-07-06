using Application.Dtos.General;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Commands
{
    public class UpdateConsoleVideoGameRequestHandler : IRequestHandler<UpdateConsoleVideoGameRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ConsoleVideoGameDto> _validator;

        private readonly ILogger<UpdateConsoleVideoGameRequestHandler> _logger;

        public UpdateConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<ConsoleVideoGameDto> validator, ILogger<UpdateConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(UpdateConsoleVideoGameRequest updateConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateConsoleVideoGame {@UpdateConsoleVideoGameRequest}.", updateConsoleVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateConsoleVideoGameRequest.ConsoleVideoGameDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ArgumentNullException(nameof(updateConsoleVideoGameRequest.ConsoleVideoGameDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleVideoGameRequest.ConsoleVideoGameDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var updatedConsoleVideoGameDto = await _unitOfWork.ConsoleVideoGameRepository.UpdateAsync(updateConsoleVideoGameRequest.ConsoleVideoGameDto);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(updatedConsoleVideoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
