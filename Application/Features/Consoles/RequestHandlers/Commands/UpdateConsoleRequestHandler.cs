using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class UpdateConsoleRequestHandler : IRequestHandler<UpdateConsoleRequest, HttpResponseDto<UpdateConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateConsoleRequestDto> _validator;

        private readonly ILogger<UpdateConsoleRequestHandler> _logger;

        public UpdateConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateConsoleRequestDto> validator, ILogger<UpdateConsoleRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UpdateConsoleResponseDto>> Handle(UpdateConsoleRequest updateConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateConsole {@UpdateConsoleRequest}.", updateConsoleRequest);

                if (updateConsoleRequest.UpdateConsoleRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateConsoleResponseDto>(new ArgumentNullException(nameof(updateConsoleRequest.UpdateConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleRequest.UpdateConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(updateConsoleRequest.UpdateConsoleRequestDto.Id);
                _mapper.Map(updateConsoleRequest.UpdateConsoleRequestDto, console);
                var updatedConsole = await _unitOfWork.ConsoleRepository.UpdateAsync(console);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<UpdateConsoleResponseDto>(new UpdateConsoleResponseDto
                {
                    Id = updatedConsole.Id,
                    UpdatedAt = updatedConsole.UpdatedAt,
                    UpdatedBy = updatedConsole.UpdatedBy,
                }, StatusCodes.Status200OK);
                _logger.LogInformation("End UpdateConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
