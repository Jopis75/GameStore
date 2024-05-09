using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewsByVideoGameIdRequestHandler : IRequestHandler<ReadReviewsByVideoGameIdRequest, HttpResponseDto<List<ReadReviewResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadReviewsByVideoGameIdRequestDto> _validator;

        private readonly ILogger<ReadReviewsByVideoGameIdRequestHandler> _logger;

        public ReadReviewsByVideoGameIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadReviewsByVideoGameIdRequestDto> validator, ILogger<ReadReviewsByVideoGameIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<ReadReviewResponseDto>>> Handle(ReadReviewsByVideoGameIdRequest readReviewsByVideoGameIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewsByVideoGameId {@ReadReviewsByVideoGameIdRequest}.", readReviewsByVideoGameIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readReviewsByVideoGameIdRequest.ReadReviewsByVideoGameIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<ReadReviewResponseDto>>(new ArgumentNullException(nameof(readReviewsByVideoGameIdRequest.ReadReviewsByVideoGameIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readReviewsByVideoGameIdRequest.ReadReviewsByVideoGameIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<ReadReviewResponseDto>>(new ArgumentNullException(nameof(readReviewsByVideoGameIdRequest.ReadReviewsByVideoGameIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviews = await _unitOfWork.ReviewRepository.ReadByVideoGameIdAsync(readReviewsByVideoGameIdRequest.ReadReviewsByVideoGameIdRequestDto.VideoGameId, true);
                var readReviewResponseDtos = reviews
                    .Select(_mapper.Map<ReadReviewResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<List<ReadReviewResponseDto>>(readReviewResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReadReviewResponseDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReadReviewResponseDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
