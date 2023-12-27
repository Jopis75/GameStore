using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadByIdConsoleRequestHandler : IRequestHandler<ReadByIdConsoleRequest, HttpResponseDto<ReadByIdConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdConsoleRequestDto> _validator;

        private readonly ILogger<ReadByIdConsoleRequestHandler> _logger;

        public ReadByIdConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdConsoleRequestDto> validator, ILogger<ReadByIdConsoleRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadByIdConsoleResponseDto>> Handle(ReadByIdConsoleRequest readByIdConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdConsole {@ReadByIdConsoleRequest}.", readByIdConsoleRequest);

                if (readByIdConsoleRequest.ReadByIdConsoleRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdConsoleResponseDto>(new ArgumentNullException(nameof(readByIdConsoleRequest.ReadByIdConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdConsoleRequest.ReadByIdConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(readByIdConsoleRequest.ReadByIdConsoleRequestDto.Id, true);
                var readByIdConsoleResponseDto = _mapper.Map<ReadByIdConsoleResponseDto>(console);

                var httpResponseDto = new HttpResponseDto<ReadByIdConsoleResponseDto>(readByIdConsoleResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadByIdConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
