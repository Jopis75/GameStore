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
    public class DeleteConsoleVideoGameRequestHandler : IRequestHandler<DeleteConsoleVideoGameRequest, HttpResponseDto<ConsoleVideoGameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ConsoleVideoGameDto> _validator;

        private readonly ILogger<DeleteConsoleVideoGameRequestHandler> _logger;

        public DeleteConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<ConsoleVideoGameDto> validator, ILogger<DeleteConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(DeleteConsoleVideoGameRequest deleteConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteConsoleVideoGame {@DeleteConsoleVideoGameRequest}.", deleteConsoleVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (deleteConsoleVideoGameRequest.Id == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ArgumentNullException(nameof(deleteConsoleVideoGameRequest.Id)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleVideoGameRequest.Id, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedConsoleVideoGameDto = await _unitOfWork.ConsoleVideoGameRepository.DeleteByIdAsync(deleteConsoleVideoGameRequest.Id ?? 0);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(deletedConsoleVideoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
