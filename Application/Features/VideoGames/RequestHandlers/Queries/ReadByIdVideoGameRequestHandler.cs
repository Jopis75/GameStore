using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadByIdVideoGameRequestHandler : IRequestHandler<ReadByIdVideoGameRequest, HttpResponseDto<ReadByIdVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdVideoGameRequestDto> _validator;

        public ReadByIdVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdVideoGameRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdVideoGameResponseDto>> Handle(ReadByIdVideoGameRequest readByIdVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdVideoGameRequest.ReadByIdVideoGameRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdVideoGameResponseDto>(new ArgumentNullException(nameof(readByIdVideoGameRequest.ReadByIdVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdVideoGameRequest.ReadByIdVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(readByIdVideoGameRequest.ReadByIdVideoGameRequestDto.Id, true);
                var readByIdVideoGameResponseDto = _mapper.Map<ReadByIdVideoGameResponseDto>(videoGame);

                return new HttpResponseDto<ReadByIdVideoGameResponseDto>(readByIdVideoGameResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
