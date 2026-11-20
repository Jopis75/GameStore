using FluentValidation;
using GameStore.Application.Features.VideoGames.Requests.Commands;
using GameStore.Application.Validators.General;

namespace GameStore.Application.Validators.Requests.VideoGames.Commands
{
    public class UploadGameStoreRequestValidator : AbstractValidator<UploadGameStoreCsvFileRequest>
    {
        public UploadGameStoreRequestValidator()
        {
            RuleFor(uploadGameStoreRequest => uploadGameStoreRequest.FormFile)
                .NotNull()
                .SetValidator(uploadGameStoreRequest => new FormFileValidator());
        }
    }
}
