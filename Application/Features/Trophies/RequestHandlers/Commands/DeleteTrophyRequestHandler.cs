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
        private readonly ITrophyRepository _trophyRepository;

        private readonly IValidator<DeleteTrophyRequest> _validator;

        private readonly ILogger<DeleteTrophyRequestHandler> _logger;

        public DeleteTrophyRequestHandler(ITrophyRepository trophyRepository, IValidator<DeleteTrophyRequest> validator, ILogger<DeleteTrophyRequestHandler> logger)
        {
            _trophyRepository = trophyRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<TrophyDto>> Handle(DeleteTrophyRequest deleteTrophyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteTrophy {@DeleteTrophyRequest}.", deleteTrophyRequest);

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

                var deletedTrophyDto = await _trophyRepository.DeleteByIdAsync(deleteTrophyRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(deletedTrophyDto, StatusCodes.Status200OK);
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
