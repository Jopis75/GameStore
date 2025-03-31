using Application.Dtos.General;
using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Commands
{
    public class CreateTrophyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateTrophyRequest> validator, ILogger<CreateTrophyRequestHandler> logger) : IRequestHandler<CreateTrophyRequest, HttpResponseDto<TrophyDto>>
    {
        public async Task<HttpResponseDto<TrophyDto>> Handle(CreateTrophyRequest createTrophyRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin CreateTrophy {@CreateTrophyRequest}.", createTrophyRequest);

                if (createTrophyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createTrophyRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(createTrophyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDto = mapper.Map<TrophyDto>(createTrophyRequest);
                var createdTrophyDto = await unitOfWork.TrophyRepository.CreateAsync(trophyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(createdTrophyDto, StatusCodes.Status201Created);
                logger.LogInformation("Done CreateTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error CreateTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
