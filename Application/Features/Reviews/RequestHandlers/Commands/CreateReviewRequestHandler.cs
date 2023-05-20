using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, HttpResponseDto<CreateReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateReviewRequestDto> _validator;

        public CreateReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateReviewRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateReviewResponseDto>> Handle(CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createReviewRequest.CreateReviewRequestDto == null)
                {
                    return new HttpResponseDto<CreateReviewResponseDto>(new ArgumentNullException(nameof(createReviewRequest.CreateReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createReviewRequest.CreateReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var review = _mapper.Map<Review>(createReviewRequest.CreateReviewRequestDto);
                review = await _unitOfWork.ReviewRepository.CreateAsync(review);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateReviewResponseDto>(new CreateReviewResponseDto
                {
                    Id = review.Id,
                    CreatedAt = review.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
