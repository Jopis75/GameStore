using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadMostPlayedVideoGameByConsoleIdRequestHandler : IRequestHandler<ReadMostPlayedVideoGameByConsoleIdRequest, HttpResponseDto<ReadVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadMostPlayedVideoGameByConsoleIdRequestDto> _validator;

        private readonly ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> _logger;

        public ReadMostPlayedVideoGameByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadMostPlayedVideoGameByConsoleIdRequestDto> validator, ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadVideoGameResponseDto>> Handle(ReadMostPlayedVideoGameByConsoleIdRequest readMostPlayedVideoGameByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadMostPlayedVideoGameByConsoleId {@ReadMostPlayedVideoGameByConsoleIdRequest}.", readMostPlayedVideoGameByConsoleIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readMostPlayedVideoGameByConsoleIdRequest.ReadMostPlayedVideoGameByConsoleIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(new ArgumentNullException(nameof(readMostPlayedVideoGameByConsoleIdRequest.ReadMostPlayedVideoGameByConsoleIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readMostPlayedVideoGameByConsoleIdRequest.ReadMostPlayedVideoGameByConsoleIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadMostPlayedByConsoleIdAsync(readMostPlayedVideoGameByConsoleIdRequest.ReadMostPlayedVideoGameByConsoleIdRequestDto.ConsoleId);
                var readVideoGameResponseDto = _mapper.Map<ReadVideoGameResponseDto>(videoGame);

                var httpResponseDto = new HttpResponseDto<ReadVideoGameResponseDto>(readVideoGameResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
