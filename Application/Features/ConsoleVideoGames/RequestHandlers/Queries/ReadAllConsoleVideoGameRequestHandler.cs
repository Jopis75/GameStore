using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleVideoGames.RequestHandlers.Queries
{
    public class ReadAllConsoleVideoGameRequestHandler : IRequestHandler<ReadAllConsoleVideoGameRequest, HttpResponseDto<ReadAllConsoleVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllConsoleVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllConsoleVideoGameResponseDto>> Handle(ReadAllConsoleVideoGameRequest readAllConsoleVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                var consoleVideoGames = await _unitOfWork.ConsoleVideoGameRepository.ReadAllAsync(true);
                var readAllConsoleVideoGameResponseDtos = consoleVideoGames
                    .Select(_mapper.Map<ReadAllConsoleVideoGameResponseDto>)
                    .ToList();

                return new HttpResponseDto<ReadAllConsoleVideoGameResponseDto>(readAllConsoleVideoGameResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllConsoleVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
