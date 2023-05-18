using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Reviews.Validators
{
    public class ReadByIdReviewRequestDtoValidator : AbstractValidator<ReadByIdReviewRequestDto>
    {
        public ReadByIdReviewRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
