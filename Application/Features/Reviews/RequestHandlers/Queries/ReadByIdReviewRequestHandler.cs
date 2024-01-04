using Application.Dtos.Common;
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
    public class ReadByIdReviewRequestHandler : IRequestHandler<ReadByIdReviewRequest, HttpResponseDto<ReadReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdReviewRequestDto> _validator;

        private readonly ILogger<ReadByIdReviewRequestHandler> _logger;

        public ReadByIdReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdReviewRequestDto> validator, ILogger<ReadByIdReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadReviewResponseDto>> Handle(ReadByIdReviewRequest readByIdReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdReview {@ReadByIdReviewRequest}.", readByIdReviewRequest);

                if (readByIdReviewRequest.ReadByIdReviewRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(new ArgumentNullException(nameof(readByIdReviewRequest.ReadByIdReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdReviewRequest.ReadByIdReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(readByIdReviewRequest.ReadByIdReviewRequestDto.Id, true);
                var readByIdReviewResponseDto = _mapper.Map<ReadReviewResponseDto>(review);

                var httpResponseDto = new HttpResponseDto<ReadReviewResponseDto>(readByIdReviewResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
