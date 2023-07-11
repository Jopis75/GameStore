using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using Application.Features.ConsoleProducts.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleProducts.RequestHandlers.Commands
{
    public class CreateConsoleProductRequestHandler : IRequestHandler<CreateConsoleProductRequest, HttpResponseDto<CreateConsoleProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateConsoleProductRequestDto> _validator;

        public CreateConsoleProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateConsoleProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateConsoleProductResponseDto>> Handle(CreateConsoleProductRequest createConsoleProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createConsoleProductRequest.CreateConsoleProductRequestDto == null)
                {
                    return new HttpResponseDto<CreateConsoleProductResponseDto>(new ArgumentNullException(nameof(createConsoleProductRequest.CreateConsoleProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createConsoleProductRequest.CreateConsoleProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateConsoleProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleProduct = _mapper.Map<ConsoleProduct>(createConsoleProductRequest.CreateConsoleProductRequestDto);
                var createdConsoleProduct = await _unitOfWork.ConsoleProductRepository.CreateAsync(consoleProduct);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateConsoleProductResponseDto>(new CreateConsoleProductResponseDto
                {
                    Id = createdConsoleProduct.Id,
                    CreatedAt = createdConsoleProduct.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateConsoleProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
