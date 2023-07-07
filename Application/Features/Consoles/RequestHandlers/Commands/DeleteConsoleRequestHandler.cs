using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class DeleteConsoleRequestHandler : IRequestHandler<DeleteConsoleRequest, HttpResponseDto<DeleteConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<DeleteConsoleRequestDto> _validator;

        public DeleteConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DeleteConsoleRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteConsoleResponseDto>> Handle(DeleteConsoleRequest deleteConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteConsoleRequest.DeleteConsoleRequestDto == null)
                {
                    return new HttpResponseDto<DeleteConsoleResponseDto>(new ArgumentNullException(nameof(deleteConsoleRequest.DeleteConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteConsoleRequest.DeleteConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(deleteConsoleRequest.DeleteConsoleRequestDto.Id);
                var deletedConsole = await _unitOfWork.ConsoleRepository.DeleteAsync(console);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteConsoleResponseDto>(new DeleteConsoleResponseDto
                {
                    Id = deletedConsole.Id,
                    DeletedAt = deletedConsole.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
