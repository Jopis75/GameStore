using Application.Dtos.General;
using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Commands
{
    public class DeleteTrophyRequestHandler : IRequestHandler<DeleteTrophyRequest, HttpResponseDto<TrophyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteTrophyRequest> _validator;

        private readonly ILogger<DeleteTrophyRequestHandler> _logger;

        public DeleteTrophyRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteTrophyRequest> validator, ILogger<DeleteTrophyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(DeleteTrophyRequest deleteTrophyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteTrophy {@DeleteTrophyRequest}.", deleteTrophyRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (deleteTrophyRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ArgumentNullException(nameof(deleteTrophyRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteTrophyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deleteTrophyDto = await _unitOfWork.TrophyRepository.DeleteByIdAsync(deleteTrophyRequest.Id, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<TrophyDto>(deleteTrophyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
