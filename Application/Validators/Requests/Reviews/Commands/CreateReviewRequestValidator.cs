using Application.Features.Reviews.Requests.Commands;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Reviews.Commands
{
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(createReviewRequest => createReviewRequest.ReviewDto)
                .NotNull()
                .SetValidator(createReviewRequest => new ReviewDtoValidator());
        }
    }
}
