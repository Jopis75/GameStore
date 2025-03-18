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
        private readonly IConsoleVideoGameRepository _consoleVideoGameRepository;

        private readonly IValidator<DeleteConsoleVideoGameRequest> _validator;

        private readonly ILogger<DeleteConsoleVideoGameRequestHandler> _logger;

        public DeleteConsoleVideoGameRequestHandler(IConsoleVideoGameRepository consoleVideoGameRepository, IValidator<DeleteConsoleVideoGameRequest> validator, ILogger<DeleteConsoleVideoGameRequestHandler> logger)
        {
            _consoleVideoGameRepository = consoleVideoGameRepository;   
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleVideoGameDto>> Handle(DeleteConsoleVideoGameRequest deleteConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteConsoleVideoGame {@DeleteConsoleVideoGameRequest}.", deleteConsoleVideoGameRequest);

                if (deleteConsoleVideoGameRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteConsoleVideoGameRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleVideoGameRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedConsoleVideoGameDto = await _consoleVideoGameRepository.DeleteByIdAsync(deleteConsoleVideoGameRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleVideoGameDto>(deletedConsoleVideoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleVideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
