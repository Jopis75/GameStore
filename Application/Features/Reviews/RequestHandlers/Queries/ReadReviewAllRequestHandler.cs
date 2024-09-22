using Application.Dtos.General;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewAllRequestHandler : IRequestHandler<ReadReviewAllRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadReviewAllRequestHandler> _logger;

        public ReadReviewAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadReviewAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(ReadReviewAllRequest readReviewAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewAll {@ReadReviewAllRequest}.", readReviewAllRequest);

                var reviewDtos = await _unitOfWork.ReviewRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(reviewDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
