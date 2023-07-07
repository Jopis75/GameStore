using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using Application.Features.ConsoleProducts.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleProducts.RequestHandlers.Queries
{
    public class ReadAllConsoleProductRequestHandler : IRequestHandler<ReadAllConsoleProductRequest, HttpResponseDto<ReadAllConsoleProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllConsoleProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllConsoleProductResponseDto>> Handle(ReadAllConsoleProductRequest readAllConsoleProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                var consoleProducts = await _unitOfWork.ConsoleProductRepository.ReadAllAsync(true);
                var readAllConsoleProductResponseDtos = consoleProducts
                    .Select(_mapper.Map<ReadAllConsoleProductResponseDto>)
                    .ToList();

                return new HttpResponseDto<ReadAllConsoleProductResponseDto>(readAllConsoleProductResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllConsoleProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
