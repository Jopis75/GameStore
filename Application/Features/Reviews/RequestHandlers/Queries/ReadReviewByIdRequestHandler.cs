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
    public class ReadReviewByIdRequestHandler : IRequestHandler<ReadReviewByIdRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IValidator<ReadReviewByIdRequest> _validator;

        private readonly ILogger<ReadReviewByIdRequestHandler> _logger;

        public ReadReviewByIdRequestHandler(IReviewRepository reviewRepository, IValidator<ReadReviewByIdRequest> validator, ILogger<ReadReviewByIdRequestHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(ReadReviewByIdRequest readReviewByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewById {@ReadReviewByIdRequest}.", readReviewByIdRequest);

                if (readReviewByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ArgumentNullException(nameof(readReviewByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readReviewByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDto = await _reviewRepository.ReadByIdAsync(readReviewByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(reviewDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
