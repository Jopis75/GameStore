using Application.Dtos.Addresses;
using Application.Dtos.Companies;
using Application.Dtos.General;
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
        [ProducesResponseType(typeof(HttpResponseDto<CreateCompanyResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CreateCompanyResponseDto>>> CreateAsync([FromBody] CreateCompanyRequestDto createCompanyRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateCompanyRequest
            {
                CompanyDto = createCompanyRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("CreateWithAddress")]
        [ProducesResponseType(typeof(HttpResponseDto<CreateCompanyWithAddressResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CreateCompanyResponseDto>>> CreateWithAddress([FromBody] CreateCompanyWithAddressRequestDto createCompanyWithAddressRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateCompanyWithHeadquarterRequest
            {
                CreateCompanyWithAddressRequestDto = createCompanyWithAddressRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<DeleteCompanyResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<DeleteCompanyResponseDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteCompanyRequest
            {
                Id = new DeleteCompanyRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(HttpResponseDto<ReadCompanyResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ReadCompanyResponseDto>>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadCompanyAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ReadCompanyResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ReadCompanyResponseDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadCompanyByIdRequest
            {
                Id = new ReadByIdRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(HttpResponseDto<UpdateCompanyResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<UpdateCompanyResponseDto>>> UpdateAsync([FromBody] UpdateCompanyRequestDto updateCompanyRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateCompanyRequest
            {
                UpdateCompanyRequestDto = updateCompanyRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
