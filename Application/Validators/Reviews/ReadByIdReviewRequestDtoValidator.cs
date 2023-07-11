using Application.Dtos.Reviews;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class ReadByIdReviewRequestDtoValidator : AbstractValidator<ReadByIdReviewRequestDto>
    {
        public ReadByIdReviewRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
