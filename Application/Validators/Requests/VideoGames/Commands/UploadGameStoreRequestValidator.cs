using Application.Features.VideoGames.Requests.Commands;
using Application.Validators.General;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Commands
{
    public class UploadGameStoreRequestValidator : AbstractValidator<UploadGameStoreRequest>
    {
        public UploadGameStoreRequestValidator()
        {
            RuleFor(uploadGameStoreRequest => uploadGameStoreRequest.FormFile)
                .NotNull()
                .SetValidator(uploadGameStoreRequest => new FormFileValidator());
        }
    }
}
