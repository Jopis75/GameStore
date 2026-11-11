using FluentValidation;
using GameStore.Application.Features.Reviews.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Reviews.Queries
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
