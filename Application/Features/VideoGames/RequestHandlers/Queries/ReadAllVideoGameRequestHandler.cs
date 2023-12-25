using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadAllVideoGameRequestHandler : IRequestHandler<ReadAllVideoGameRequest, HttpResponseDto<ReadAllVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllVideoGameResponseDto>> Handle(ReadAllVideoGameRequest readAllVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                var videoGames = await _unitOfWork.VideoGameRepository.ReadAllAsync(true);
                var readAllVideoGamesResponseDtos = videoGames
                    .Select(_mapper.Map<ReadAllVideoGameResponseDto>)
                    .ToList();

                return new HttpResponseDto<ReadAllVideoGameResponseDto>(readAllVideoGamesResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
