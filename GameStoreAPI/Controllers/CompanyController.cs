using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Features.Companies.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(CreateCompanyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateAddressResponseDto>> CreateAsync([FromBody] CreateCompanyRequestDto createCompanyRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateCompanyRequest
            {
                CreateCompanyRequestDto = createCompanyRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(DeleteCompanyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteCompanyResponseDto>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteCompanyRequest
            {
                DeleteCompanyRequestDto = new DeleteCompanyRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(ReadCompanyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadCompanyResponseDto>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadCompanyAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(ReadCompanyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadCompanyResponseDto>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadCompanyByIdRequest
            {
                ReadByIdRequestDto = new ReadByIdRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(UpdateCompanyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateCompanyResponseDto>> UpdateAsync([FromBody] UpdateCompanyRequestDto updateCompanyRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateCompanyRequest
            {
                UpdateCompanyRequestDto = updateCompanyRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
