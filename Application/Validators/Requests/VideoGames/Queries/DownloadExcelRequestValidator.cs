using Application.Features.VideoGames.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Queries
{
    public class DownloadExcelRequestValidator : AbstractValidator<DownloadExcelRequest>
    {
        public DownloadExcelRequestValidator()
        {
            RuleFor(downloadExcelRequest => downloadExcelRequest.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(downloadExcelRequest => downloadExcelRequest.FileDownloadName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
