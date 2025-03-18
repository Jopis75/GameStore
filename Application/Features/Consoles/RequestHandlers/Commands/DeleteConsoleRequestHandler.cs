using Application.Dtos.General;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class DeleteConsoleRequestHandler : IRequestHandler<DeleteConsoleRequest, HttpResponseDto<ConsoleDto>>
    {
        private readonly IConsoleRepository _consoleRepository;

        private readonly IValidator<DeleteConsoleRequest> _validator;

        private readonly ILogger<DeleteConsoleRequestHandler> _logger;

        public DeleteConsoleRequestHandler(IConsoleRepository consoleRepository, IValidator<DeleteConsoleRequest> validator, ILogger<DeleteConsoleRequestHandler> logger)
        {
            _consoleRepository = consoleRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(DeleteConsoleRequest deleteConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteConsole {@DeleteConsoleRequest}.", deleteConsoleRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (deleteConsoleRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteConsoleRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedConsoleDto = await _consoleRepository.DeleteByIdAsync(deleteConsoleRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(deletedConsoleDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
