using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, HttpResponseDto<DeleteReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteReviewRequestDto> _validator;

        private readonly ILogger<DeleteReviewRequestHandler> _logger;

        public DeleteReviewRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteReviewRequestDto> validator, ILogger<DeleteReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DeleteReviewResponseDto>> Handle(DeleteReviewRequest deleteReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteReview {@DeleteReviewRequest}.", deleteReviewRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (deleteReviewRequest.DeleteReviewRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteReviewResponseDto>(new ArgumentNullException(nameof(deleteReviewRequest.DeleteReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteReviewRequest.DeleteReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(deleteReviewRequest.DeleteReviewRequestDto.Id);
                var deletedReview = await _unitOfWork.ReviewRepository.DeleteAsync(review);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<DeleteReviewResponseDto>(new DeleteReviewResponseDto
                {
                    Id = deletedReview.Id,
                    DeletedAt = deletedReview.DeletedAt,
                    DeletedBy = deletedReview.DeletedBy
                }, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
