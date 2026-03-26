using Gerenciamento_Cursos.Applications.Services;
using Gerenciamento_Cursos.DTOs.Instrutor;
using Gerenciamento_Cursos.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_Cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrutorController : ControllerBase
    {
        private readonly InstrutorService _service;

        public InstrutorController(InstrutorService service)
        {
            _service = service; 
        }

        [HttpGet]
        public ActionResult<List<LerInstrutorDto>> Listar()
        {
            List<LerInstrutorDto> instrutores = _service.Listar();

            return Ok(instrutores);
        }

        [HttpGet("{id}")]
        public ActionResult<LerInstrutorDto> BuscarPorId(int id)
        {
            try
            {
                LerInstrutorDto instrutor = _service.ObterPorId(id);
                return Ok(instrutor);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarInstrutorDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Ok(dto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
