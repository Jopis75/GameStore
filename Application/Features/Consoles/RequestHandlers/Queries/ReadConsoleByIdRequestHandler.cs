using Application.Dtos.General;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadConsoleByIdRequestHandler : IRequestHandler<ReadConsoleByIdRequest, HttpResponseDto<ConsoleDto>>
    {
        private readonly IConsoleRepository _consoleRepository;

        private readonly IValidator<ReadConsoleByIdRequest> _validator;

        private readonly ILogger<ReadConsoleByIdRequestHandler> _logger;

        public ReadConsoleByIdRequestHandler(IConsoleRepository consoleRepository, IValidator<ReadConsoleByIdRequest> validator, ILogger<ReadConsoleByIdRequestHandler> logger)
        {
            _consoleRepository = consoleRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(ReadConsoleByIdRequest readConsoleByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleById {@ReadConsoleByIdRequest}.", readConsoleByIdRequest);

                if (readConsoleByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readConsoleByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readConsoleByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDto = await _consoleRepository.ReadByIdAsync(readConsoleByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(consoleDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
