using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class DeleteConsoleRequestHandler : IRequestHandler<DeleteConsoleRequest, HttpResponseDto<DeleteConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteConsoleRequestDto> _validator;

        private readonly ILogger<DeleteConsoleRequestHandler> _logger;

        public DeleteConsoleRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteConsoleRequestDto> validator, ILogger<DeleteConsoleRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DeleteConsoleResponseDto>> Handle(DeleteConsoleRequest deleteConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteConsole {@DeleteConsoleRequest}.", deleteConsoleRequest);

                if (deleteConsoleRequest.DeleteConsoleRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteConsoleResponseDto>(new ArgumentNullException(nameof(deleteConsoleRequest.DeleteConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleRequest.DeleteConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(deleteConsoleRequest.DeleteConsoleRequestDto.Id);
                var deletedConsole = await _unitOfWork.ConsoleRepository.DeleteAsync(console);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<DeleteConsoleResponseDto>(new DeleteConsoleResponseDto
                {
                    Id = deletedConsole.Id,
                    DeletedAt = deletedConsole.DeletedAt,
                    DeletedBy = deletedConsole.DeletedBy,
                }, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
