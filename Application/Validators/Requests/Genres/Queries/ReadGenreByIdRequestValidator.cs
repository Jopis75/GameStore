using Application.Features.Genres.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Queries
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
