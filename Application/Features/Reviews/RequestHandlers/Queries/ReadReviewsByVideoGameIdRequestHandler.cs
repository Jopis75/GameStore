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
    public class ReadReviewsByVideoGameIdRequestHandler : IRequestHandler<ReadReviewsByVideoGameIdRequest, HttpResponseDto<List<ReviewDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadReviewsByVideoGameIdRequest> _validator;

        private readonly ILogger<ReadReviewsByVideoGameIdRequestHandler> _logger;

        public ReadReviewsByVideoGameIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadReviewsByVideoGameIdRequest> validator, ILogger<ReadReviewsByVideoGameIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<ReviewDto>>> Handle(ReadReviewsByVideoGameIdRequest readReviewsByVideoGameIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewsByVideoGameId {@ReadReviewsByVideoGameIdRequest}.", readReviewsByVideoGameIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readReviewsByVideoGameIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<ReviewDto>>(new ArgumentNullException(nameof(readReviewsByVideoGameIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readReviewsByVideoGameIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<ReviewDto>>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDtos = await _unitOfWork.ReviewRepository.ReadByVideoGameIdAsync(readReviewsByVideoGameIdRequest.VideoGameId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<List<ReviewDto>>(reviewDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReviewDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReviewDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
