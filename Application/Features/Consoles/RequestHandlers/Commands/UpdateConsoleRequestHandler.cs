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
    public class UpdateConsoleRequestHandler : IRequestHandler<UpdateConsoleRequest, HttpResponseDto<UpdateConsoleResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateConsoleRequestDto> _validator;

        public UpdateConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateConsoleRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateConsoleResponseDto>> Handle(UpdateConsoleRequest updateConsoleRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateConsoleRequest.UpdateConsoleRequestDto == null)
                {
                    return new HttpResponseDto<UpdateConsoleResponseDto>(new ArgumentNullException(nameof(updateConsoleRequest.UpdateConsoleRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateConsoleRequest.UpdateConsoleRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateConsoleResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var console = await _unitOfWork.ConsoleRepository.ReadByIdAsync(updateConsoleRequest.UpdateConsoleRequestDto.Id);
                _mapper.Map(updateConsoleRequest.UpdateConsoleRequestDto, console);
                var updatedConsole = await _unitOfWork.ConsoleRepository.UpdateAsync(console);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<UpdateConsoleResponseDto>(new UpdateConsoleResponseDto
                {
                    Id = updatedConsole.Id,
                    UpdatedAt = updatedConsole.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateConsoleResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
