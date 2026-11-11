using FluentValidation;
using GameStore.Application.Features.Reviews.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Reviews.Queries
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
