using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Console = Domain.Entities.Console;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class CreateConsoleRequestHandler : IRequestHandler<CreateConsoleRequest, HttpResponseDto<CreateConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleRequestDto> _validator;

        private readonly ILogger<CreateConsoleRequestHandler> _logger;

        public CreateConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateConsoleRequestDto> validator, ILogger<CreateConsoleRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateConsoleResponseDto>> Handle(CreateConsoleRequest createConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateConsole {@CreateConsoleRequest}.", createConsoleRequest);

                if (createConsoleRequest.CreateConsoleRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateConsoleResponseDto>(new ArgumentNullException(nameof(createConsoleRequest.CreateConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createConsoleRequest.CreateConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var console = _mapper.Map<Console>(createConsoleRequest.CreateConsoleRequestDto);
                var createdConsole = await _unitOfWork.ConsoleRepository.CreateAsync(console);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateConsoleResponseDto>(new CreateConsoleResponseDto
                {
                    Id = createdConsole.Id,
                    CreatedAt = createdConsole.CreatedAt,
                    CreatedBy = createdConsole.CreatedBy,
                }, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
