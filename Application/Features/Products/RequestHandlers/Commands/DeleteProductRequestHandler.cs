using Application.Dtos.Common;
using Application.Dtos.Products;
using Application.Features.Products.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.RequestHandlers.Commands
{
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, HttpResponseDto<DeleteProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteProductRequestDto> _validator;

        public DeleteProductRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteProductResponseDto>> Handle(DeleteProductRequest deleteProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteProductRequest.DeleteProductRequestDto == null)
                {
                    return new HttpResponseDto<DeleteProductResponseDto>(new ArgumentNullException(nameof(deleteProductRequest.DeleteProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteProductRequest.DeleteProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var product = await _unitOfWork.ProductRepository.ReadByIdAsync(deleteProductRequest.DeleteProductRequestDto.Id);
                product = await _unitOfWork.ProductRepository.DeleteAsync(product);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteProductResponseDto>(new DeleteProductResponseDto
                {
                    Id = product.Id,
                    DeletedAt = product.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
