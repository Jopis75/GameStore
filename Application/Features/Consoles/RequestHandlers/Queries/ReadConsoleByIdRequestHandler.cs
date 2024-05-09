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
    public class ReadConsoleByIdRequestHandler : IRequestHandler<ReadConsoleByIdRequest, HttpResponseDto<ReadConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdRequestDto> _validator;

        private readonly ILogger<ReadConsoleByIdRequestHandler> _logger;

        public ReadConsoleByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdRequestDto> validator, ILogger<ReadConsoleByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadConsoleResponseDto>> Handle(ReadConsoleByIdRequest readConsoleByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadConsoleById {@ReadConsoleByIdRequest}.", readConsoleByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readConsoleByIdRequest.ReadByIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadConsoleResponseDto>(new ArgumentNullException(nameof(readConsoleByIdRequest.ReadByIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readConsoleByIdRequest.ReadByIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(readConsoleByIdRequest.ReadByIdRequestDto.Id, true);
                var readConsoleResponseDto = _mapper.Map<ReadConsoleResponseDto>(console);

                var httpResponseDto = new HttpResponseDto<ReadConsoleResponseDto>(readConsoleResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadConsoleById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
