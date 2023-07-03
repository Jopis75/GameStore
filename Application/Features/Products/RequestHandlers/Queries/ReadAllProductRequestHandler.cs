using Application.Dtos.Common;
using Application.Dtos.Products;
using Application.Features.Products.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.RequestHandlers.Queries
{
    public class ReadAllProductRequestHandler : IRequestHandler<ReadAllProductRequest, HttpResponseDto<ReadAllProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllProductResponseDto>> Handle(ReadAllProductRequest readAllProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.ReadAllAsync(true);
                var readAllProductsResponseDtos = products.Select(_mapper.Map<ReadAllProductResponseDto>).ToList();

                return new HttpResponseDto<ReadAllProductResponseDto>(readAllProductsResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
