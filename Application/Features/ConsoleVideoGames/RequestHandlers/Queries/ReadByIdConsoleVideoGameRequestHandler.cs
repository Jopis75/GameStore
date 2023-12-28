using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadByIdConsoleVideoGameRequestHandler : IRequestHandler<ReadByIdConsoleVideoGameRequest, HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdConsoleVideoGameRequestDto> _validator;

        private readonly ILogger<ReadByIdConsoleVideoGameRequestHandler> _logger;

        public ReadByIdConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdConsoleVideoGameRequestDto> validator, ILogger<ReadByIdConsoleVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>> Handle(ReadByIdConsoleVideoGameRequest readByIdConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdConsoleVideoGame {@ReadByIdConsoleVideoGameRequest}.", readByIdConsoleVideoGameRequest);

                if (readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(readByIdConsoleVideoGameRequest.ReadByIdConsoleVideoGameRequestDto.Id, true);
                var readByIdConsoleVideoGameResponseDto = _mapper.Map<ReadByIdConsoleVideoGameResponseDto>(consoleVideoGame);

                var httpResponseDto = new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(readByIdConsoleVideoGameResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdConsolevideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadByIdConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
