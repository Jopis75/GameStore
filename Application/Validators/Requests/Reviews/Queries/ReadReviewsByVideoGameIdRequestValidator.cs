using Application.Features.Reviews.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Reviews.Queries
{
    public class ReadReviewsByVideoGameIdRequestValidator : AbstractValidator<ReadReviewsByVideoGameIdRequest>
    {
        public ReadReviewsByVideoGameIdRequestValidator()
        {
            RuleFor(readReviewVideoGameByIdRequest => readReviewVideoGameByIdRequest.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
