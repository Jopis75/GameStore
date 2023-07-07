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
    public class DeleteConsoleProductRequestHandler : IRequestHandler<DeleteConsoleProductRequest, HttpResponseDto<DeleteConsoleProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<DeleteConsoleProductRequestDto> _validator;

        public DeleteConsoleProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DeleteConsoleProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteConsoleProductResponseDto>> Handle(DeleteConsoleProductRequest deleteConsoleProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteConsoleProductRequest.DeleteConsoleProductRequestDto == null)
                {
                    return new HttpResponseDto<DeleteConsoleProductResponseDto>(new ArgumentNullException(nameof(deleteConsoleProductRequest.DeleteConsoleProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleProductRequest.DeleteConsoleProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteConsoleProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleProduct = await _unitOfWork.ConsoleProductRepository.ReadByIdAsync(deleteConsoleProductRequest.DeleteConsoleProductRequestDto.Id);
                var deletedConsole = await _unitOfWork.ConsoleProductRepository.DeleteAsync(consoleProduct);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteConsoleProductResponseDto>(new DeleteConsoleProductResponseDto
                {
                    Id = deletedConsole.Id,
                    DeletedAt = deletedConsole.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteConsoleProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
