using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Application.Commands.ClienteCommands;
using ClientesCRM.src.Application.Queries.ClienteQueries;

namespace ClientesCRM.src.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClienteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteResponseDto>> GetById(Guid id)
        {

            var cliente = await _mediator.Send(new GetClienteByIdQuery(id));

            var dto = _mapper.Map<ClienteResponseDto>(cliente);

            return Ok(dto);
        }

        // GET api/clientes
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClienteResponseDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllClientesQuery());
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<ClienteResponseDto>> Create([FromBody] ClienteCreateDto dto)
        {

            var command = _mapper.Map<ClienteCreateCommand>(dto);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteResponseDto>> Update(Guid id, [FromBody] ClienteUpdateDto dto)
        {

            var command = new ClienteUpdateCommand(
                id,
                dto.Telefono,
                dto.CorreoElectronico,
                dto.DireccionPrincipal
            );

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new ClienteDeleteCommand(id));
            return NoContent();
        }
    }
}
