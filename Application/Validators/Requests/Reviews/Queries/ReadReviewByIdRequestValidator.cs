using Application.Features.Reviews.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Reviews.Queries
{
    public class ReadReviewByIdRequestValidator : AbstractValidator<ReadReviewByIdRequest>
    {
        public ReadReviewByIdRequestValidator()
        {
            RuleFor(readReviewByIdRequest => readReviewByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
