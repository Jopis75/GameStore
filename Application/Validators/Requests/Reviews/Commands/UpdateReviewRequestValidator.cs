using Application.Features.Reviews.Requests.Commands;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Reviews.Commands
{
    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidator()
        {
            RuleFor(updateReviewRequest => updateReviewRequest.ReviewDto)
                .NotNull()
                .SetValidator(updateReviewRequest => new ReviewDtoValidator());
        }
    }
}
