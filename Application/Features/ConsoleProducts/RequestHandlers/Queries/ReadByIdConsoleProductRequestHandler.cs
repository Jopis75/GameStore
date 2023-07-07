using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using Application.Features.ConsoleProducts.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ConsoleProducts.RequestHandlers.Queries
{
    public class ReadByIdConsoleProductRequestHandler : IRequestHandler<ReadByIdConsoleProductRequest, HttpResponseDto<ReadByIdConsoleProductResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdConsoleProductRequestDto> _validator;

        public ReadByIdConsoleProductRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdConsoleProductRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdConsoleProductResponseDto>> Handle(ReadByIdConsoleProductRequest readByIdConsoleProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdConsoleProductRequest.ReadByIdConsoleProductRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdConsoleProductResponseDto>(new ArgumentNullException(nameof(readByIdConsoleProductRequest.ReadByIdConsoleProductRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdConsoleProductRequest.ReadByIdConsoleProductRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdConsoleProductResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var consoleProduct = await _unitOfWork.ConsoleProductRepository.ReadByIdAsync(readByIdConsoleProductRequest.ReadByIdConsoleProductRequestDto.Id, true);
                var readByIdConsoleProductResponseDto = _mapper.Map<ReadByIdConsoleProductResponseDto>(consoleProduct);

                return new HttpResponseDto<ReadByIdConsoleProductResponseDto>(readByIdConsoleProductResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdConsoleProductResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
