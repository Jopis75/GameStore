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
    public class UpdateConsoleRequestHandler : IRequestHandler<UpdateConsoleRequest, HttpResponseDto<ConsoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<UpdateConsoleRequest> _validator;

        private readonly ILogger<UpdateConsoleRequestHandler> _logger;

        public UpdateConsoleRequestHandler(IUnitOfWork unitOfWork, IValidator<UpdateConsoleRequest> validator, ILogger<UpdateConsoleRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ConsoleDto>> Handle(UpdateConsoleRequest updateConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateConsole {@UpdateConsoleRequest}.", updateConsoleRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateConsoleRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(new ArgumentNullException(nameof(updateConsoleRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var updatedConsoleDto = await _unitOfWork.ConsoleRepository.UpdateAsync(updateConsoleRequest.ConsoleDto, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(updatedConsoleDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
