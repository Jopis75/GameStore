using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadByIdReviewRequestHandler : IRequestHandler<ReadByIdReviewRequest, HttpResponseDto<ReadByIdReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdReviewRequestDto> _validator;

        public ReadByIdReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdReviewRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdReviewResponseDto>> Handle(ReadByIdReviewRequest readByIdReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdReviewRequest.ReadByIdReviewRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdReviewResponseDto>(new ArgumentNullException(nameof(readByIdReviewRequest.ReadByIdReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdReviewRequest.ReadByIdReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(readByIdReviewRequest.ReadByIdReviewRequestDto.Id, true);
                var readByIdReviewResponseDto = _mapper.Map<ReadByIdReviewResponseDto>(review);

                return new HttpResponseDto<ReadByIdReviewResponseDto>(readByIdReviewResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
