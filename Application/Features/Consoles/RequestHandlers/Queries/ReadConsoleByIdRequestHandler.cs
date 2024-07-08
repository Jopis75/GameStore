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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadConsoleByIdRequest> _validator;

        private readonly ILogger<ReadConsoleByIdRequestHandler> _logger;

        public ReadConsoleByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadConsoleByIdRequest> validator, ILogger<ReadConsoleByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(ReadConsoleByIdRequest readConsoleByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleById {@ReadConsoleByIdRequest}.", readConsoleByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readConsoleByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(new ArgumentNullException(nameof(readConsoleByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readConsoleByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDto = await _unitOfWork.ConsoleRepository.ReadByIdAsync(readConsoleByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(consoleDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
