using Application.Dtos.General;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewsByVideoGameIdRequestHandler : IRequestHandler<ReadReviewsByVideoGameIdRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IValidator<ReadReviewsByVideoGameIdRequest> _validator;

        private readonly ILogger<ReadReviewsByVideoGameIdRequestHandler> _logger;

        public ReadReviewsByVideoGameIdRequestHandler(IReviewRepository reviewRepository, IValidator<ReadReviewsByVideoGameIdRequest> validator, ILogger<ReadReviewsByVideoGameIdRequestHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(ReadReviewsByVideoGameIdRequest readReviewsByVideoGameIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewsByVideoGameId {@ReadReviewsByVideoGameIdRequest}.", readReviewsByVideoGameIdRequest);

                if (readReviewsByVideoGameIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readReviewsByVideoGameIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readReviewsByVideoGameIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDtos = await _reviewRepository.ReadByVideoGameIdAsync(readReviewsByVideoGameIdRequest.VideoGameId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(reviewDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
