using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using Application.Features.ConsoleProducts.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleProducts.RequestHandlers.Commands
{
    public class UpdateConsoleProductRequestHandler : IRequestHandler<UpdateConsoleProductRequest, HttpResponseDto<UpdateConsoleProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateConsoleProductRequestDto> _validator;

        public UpdateConsoleProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateConsoleProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateConsoleProductResponseDto>> Handle(UpdateConsoleProductRequest updateConsoleProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateConsoleProductRequest.UpdateConsoleProductRequestDto == null)
                {
                    return new HttpResponseDto<UpdateConsoleProductResponseDto>(new ArgumentNullException(nameof(updateConsoleProductRequest.UpdateConsoleProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleProductRequest.UpdateConsoleProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateConsoleProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleProduct = await _unitOfWork.ConsoleProductRepository.ReadByIdAsync(updateConsoleProductRequest.UpdateConsoleProductRequestDto.Id);
                _mapper.Map(updateConsoleProductRequest.UpdateConsoleProductRequestDto, consoleProduct);
                var updatedConsoleProduct = await _unitOfWork.ConsoleProductRepository.UpdateAsync(consoleProduct);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<UpdateConsoleProductResponseDto>(new UpdateConsoleProductResponseDto
                {
                    Id = updatedConsoleProduct.Id,
                    UpdatedAt = updatedConsoleProduct.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateConsoleProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
