using Gerenciamento_Cursos.Applications.Regras;
using Gerenciamento_Cursos.Domains;
using Gerenciamento_Cursos.DTOs.Instrutor;
using Gerenciamento_Cursos.Exceptions;
using Gerenciamento_Cursos.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Gerenciamento_Cursos.Applications.Services
{
    public class InstrutorService
    {
        private readonly IInstrutorRepository _repository;

        public InstrutorService(IInstrutorRepository repository)
        {
            _repository = repository;
        }
        private static LerInstrutorDto LerDto(Instrutor instrutor) // pega a entidade usuario e gera um DTO
        {
            LerInstrutorDto LerInstrutor = new LerInstrutorDto
            {
                InstrutorID = instrutor.InstrutorID,
                Nome = instrutor.Nome,
                Email = instrutor.Email,    
                Senha = instrutor.Senha,
                AreaEspecializacaoID = instrutor.AreaEspecializacaoID
            };
            return LerInstrutor;
        }
        public List<LerInstrutorDto> Listar()
        {
            List<Instrutor> instrutores = _repository.Listar();

            List<LerInstrutorDto> instrutoresDtos = instrutores.Select(instrutorBanco => LerDto(instrutorBanco)).ToList();  
            
            return instrutoresDtos;
        }


        public LerInstrutorDto ObterPorId(int id)
        {
            Instrutor instrutor = _repository.ObterPorId(id);

            if (instrutor == null)
            {
                throw new DomainException("Instrutor nao encontrado");
            }

            return LerDto(instrutor);
        }

        public LerInstrutorDto ObterPorEmail(string email)
        {
            Instrutor instrutor = _repository.ObterPorEmail(email);

            if (instrutor == null)
            {
                throw new DomainException("Instutor nao encontrado");
            }

            return LerDto(instrutor);
        }

        public LerInstrutorDto Adicionar(CriarInstrutorDto dto)
        {
            Validar.ValidarNome(dto.Nome);

            Instrutor instrutorExistente = _repository.ObterPorEmail(dto.Email);

            if (instrutorExistente != null)
            {
                throw new DomainException("Ja existe um instrutor cadastrado com esse email");
            }

            Instrutor instrutor = new Instrutor()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha= dto.Senha,
                AreaEspecializacaoID = dto.AreaEspecializacaoID
            };

            _repository.Adicionar(instrutor);

            return LerDto(instrutor);
        }

        public LerInstrutorDto Atualizar(CriarInstrutorDto dto, int id)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id);

            if (instrutorBanco == null)
            {
                throw new DomainException("Instrutor nao encontrado");
            }

            Validar.ValidarEmail(dto.Email);

            Instrutor instrutorComMesmoEmail = _repository.ObterPorEmail(dto.Email);

            if (instrutorComMesmoEmail != null && instrutorComMesmoEmail.InstrutorID != id)
            {
                throw new DomainException("Ja existe instrutor com esse email");
            }

            instrutorBanco.Nome = dto.Nome;
            instrutorBanco.Email = dto.Email;
            instrutorBanco.Senha = dto.Senha;
            instrutorBanco.AreaEspecializacaoID = dto.AreaEspecializacaoID;

            _repository.Atualizar(instrutorBanco);

            return LerDto(instrutorBanco);
        }

    }
}
