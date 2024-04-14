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
    public class ReadVideoGameByConsoleIdRequestHandler : IRequestHandler<ReadVideoGameByConsoleIdRequest, HttpResponseDto<List<ReadVideoGameResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadVideoGameByConsoleIdRequestDto> _validator;

        private readonly ILogger<ReadVideoGameByConsoleIdRequestHandler> _logger;

        public ReadVideoGameByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadVideoGameByConsoleIdRequestDto> validator, ILogger<ReadVideoGameByConsoleIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<ReadVideoGameResponseDto>>> Handle(ReadVideoGameByConsoleIdRequest readVideoGameByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGameByConsoleId {@ReadVideoGameByConsoleIdRequest}.", readVideoGameByConsoleIdRequest);

                if (readVideoGameByConsoleIdRequest.ReadVideoGameByConsoleIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<ReadVideoGameResponseDto>>(new ArgumentNullException(nameof(readVideoGameByConsoleIdRequest.ReadVideoGameByConsoleIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readVideoGameByConsoleIdRequest.ReadVideoGameByConsoleIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<ReadVideoGameResponseDto>>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGames = await _unitOfWork.VideoGameRepository.ReadByConsoleIdAsync(readVideoGameByConsoleIdRequest.ReadVideoGameByConsoleIdRequestDto.ConsoleId, true);
                var readVideoGameResponseDtos = videoGames
                    .Select(_mapper.Map<ReadVideoGameResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<List<ReadVideoGameResponseDto>>(readVideoGameResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReadVideoGameResponseDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
