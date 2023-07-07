using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadAllConsoleRequestHandler : IRequestHandler<ReadAllConsoleRequest, HttpResponseDto<ReadAllConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllConsoleResponseDto>> Handle(ReadAllConsoleRequest readAllConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                var consoles = await _unitOfWork.ConsoleRepository.ReadAllAsync(true);
                var readAllConsoleResponseDtos = consoles
                    .Select(_mapper.Map<ReadAllConsoleResponseDto>)
                    .ToList();

                return new HttpResponseDto<ReadAllConsoleResponseDto>(readAllConsoleResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
