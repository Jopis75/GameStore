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
    public class ReadReviewByIdRequestHandler : IRequestHandler<ReadReviewByIdRequest, HttpResponseDto<ReadReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadReviewByIdRequestDto> _validator;

        private readonly ILogger<ReadReviewByIdRequestHandler> _logger;

        public ReadReviewByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadReviewByIdRequestDto> validator, ILogger<ReadReviewByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadReviewResponseDto>> Handle(ReadReviewByIdRequest readByIdReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdReview {@ReadByIdReviewRequest}.", readByIdReviewRequest);

                if (readByIdReviewRequest.ReadReviewByIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(new ArgumentNullException(nameof(readByIdReviewRequest.ReadReviewByIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdReviewRequest.ReadReviewByIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(readByIdReviewRequest.ReadReviewByIdRequestDto.Id, true);
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
