using Application.Dtos.Common;
using Application.Dtos.Products;
using Application.Features.Products.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.RequestHandlers.Commands
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, HttpResponseDto<CreateProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateProductRequestDto> _validator;

        public CreateProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateProductResponseDto>> Handle(CreateProductRequest createProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createProductRequest.CreateProductRequestDto == null)
                {
                    return new HttpResponseDto<CreateProductResponseDto>(new ArgumentNullException(nameof(createProductRequest.CreateProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createProductRequest.CreateProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var product = _mapper.Map<Product>(createProductRequest.CreateProductRequestDto);
                product = await _unitOfWork.ProductRepository.CreateAsync(product);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateProductResponseDto>(new CreateProductResponseDto
                {
                    Id = product.Id,
                    CreatedAt = product.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
