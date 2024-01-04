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
    public class ReadConsoleVideoGameByIdRequestHandler : IRequestHandler<ReadConsoleVideoGameByIdRequest, HttpResponseDto<ReadConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadConsoleVideoGameByIdRequestDto> _validator;

        private readonly ILogger<ReadConsoleVideoGameByIdRequestHandler> _logger;

        public ReadConsoleVideoGameByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadConsoleVideoGameByIdRequestDto> validator, ILogger<ReadConsoleVideoGameByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadConsoleVideoGameResponseDto>> Handle(ReadConsoleVideoGameByIdRequest readByIdConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdConsoleVideoGame {@ReadByIdConsoleVideoGameRequest}.", readByIdConsoleVideoGameRequest);

                if (readByIdConsoleVideoGameRequest.ReadConsoleVideoGameByIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadConsoleVideoGameResponseDto>(new ArgumentNullException(nameof(readByIdConsoleVideoGameRequest.ReadConsoleVideoGameByIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdConsoleVideoGameRequest.ReadConsoleVideoGameByIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadConsoleVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleVideoGame = await _unitOfWork.ConsoleVideoGameRepository.ReadByIdAsync(readByIdConsoleVideoGameRequest.ReadConsoleVideoGameByIdRequestDto.Id, true);
                var readByIdConsoleVideoGameResponseDto = _mapper.Map<ReadConsoleVideoGameResponseDto>(consoleVideoGame);

                var httpResponseDto = new HttpResponseDto<ReadConsoleVideoGameResponseDto>(readByIdConsoleVideoGameResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdConsolevideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdConsoleVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
