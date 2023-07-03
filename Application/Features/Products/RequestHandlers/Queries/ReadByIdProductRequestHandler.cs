using Application.Dtos.Common;
using Application.Dtos.Products;
using Application.Features.Products.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.RequestHandlers.Queries
{
    public class ReadByIdProductRequestHandler : IRequestHandler<ReadByIdProductRequest, HttpResponseDto<ReadByIdProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdProductRequestDto> _validator;

        public ReadByIdProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdProductResponseDto>> Handle(ReadByIdProductRequest readByIdProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdProductRequest.ReadByIdProductRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdProductResponseDto>(new ArgumentNullException(nameof(readByIdProductRequest.ReadByIdProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdProductRequest.ReadByIdProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var product = await _unitOfWork.ProductRepository.ReadByIdAsync(readByIdProductRequest.ReadByIdProductRequestDto.Id, true);
                var readByIdProductResponseDto = _mapper.Map<ReadByIdProductResponseDto>(product);

                return new HttpResponseDto<ReadByIdProductResponseDto>(readByIdProductResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
