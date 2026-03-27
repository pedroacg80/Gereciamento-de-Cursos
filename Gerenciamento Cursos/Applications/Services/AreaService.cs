using Gerenciamento_Cursos.Applications.Regras;
using Gerenciamento_Cursos.Domains;
using Gerenciamento_Cursos.DTOs.AreaEspecializacao;
using Gerenciamento_Cursos.DTOs.Instrutor;
using Gerenciamento_Cursos.Exceptions;
using Gerenciamento_Cursos.Repositories;

namespace Gerenciamento_Cursos.Applications.Services
{
    public class AreaService
    {
        private readonly AreaRepository _repository;

        public AreaService(AreaRepository repository)
        {
            _repository = repository;
        }

        private static LerAreaDto LerDto(AreaEspecializacao area)
        {
            LerAreaDto lerArea = new LerAreaDto
            {
                AreaId = area.AreaID,
                Nome = area.Nome
            };

            return lerArea;
        }
        public List<LerAreaDto> Listar ()
        {
            List<AreaEspecializacao> areas = _repository.Listar();

            List<LerAreaDto> areaDto = areas.Select(a => LerDto(a)).ToList();

            return areaDto;
        }

        public LerAreaDto ObterPorId(int id)
        {
            AreaEspecializacao area = _repository.ObterPorId(id);

            if (area == null)
            {
                throw new DomainException("Area nao encontrada");
            }
            
            return LerDto(area);
        }

        public void Adicionar(CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.Nome);

            bool areaExistente = _repository.AreaExiste(dto.Nome);

            if (areaExistente != null)
            {
                throw new DomainException("Ja existe uma area com esse nome");
            }


        }
    }
}
