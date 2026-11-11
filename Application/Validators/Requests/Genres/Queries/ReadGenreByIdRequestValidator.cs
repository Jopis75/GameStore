using FluentValidation;
using GameStore.Application.Features.Genres.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Genres.Queries
{
    public class ReadGenreByIdRequestValidator : AbstractValidator<ReadGenreByIdRequest>
    {
        public ReadGenreByIdRequestValidator()
        {
            RuleFor(readGenreByIdRequest => readGenreByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
