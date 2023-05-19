using Application.Dtos.Common;
using Application.Dtos.Products;
using Application.Features.Products.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.RequestHandlers.Commands
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, HttpResponseDto<UpdateProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateProductRequestDto> _validator;

        public UpdateProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateProductResponseDto>> Handle(UpdateProductRequest updateProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateProductRequest.UpdateProductRequestDto == null)
                {
                    return new HttpResponseDto<UpdateProductResponseDto>(new ArgumentNullException(nameof(updateProductRequest.UpdateProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateProductRequest.UpdateProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var product = await _unitOfWork.ProductRepository.ReadByIdAsync(updateProductRequest.UpdateProductRequestDto.Id);
                _mapper.Map(updateProductRequest.UpdateProductRequestDto, product);
                product = await _unitOfWork.ProductRepository.UpdateAsync(product);

                return new HttpResponseDto<UpdateProductResponseDto>(new UpdateProductResponseDto
                {
                    Id = product.Id,
                    UpdatedAt = product.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
