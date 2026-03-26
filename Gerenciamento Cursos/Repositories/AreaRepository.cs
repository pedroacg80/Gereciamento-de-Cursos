using Gerenciamento_Cursos.Contexts;
using Gerenciamento_Cursos.Domains;
using Gerenciamento_Cursos.Interfaces;

namespace Gerenciamento_Cursos.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly GerenciamentoCursosContext _context;

        public AreaRepository(GerenciamentoCursosContext context)
        {
            _context = context;
        }

        public List<AreaEspecializacao> Listar()
        {
            return _context.AreaEspecializacao.ToList();
        }

        public AreaEspecializacao? ObterPorId(int id)
        {
            return _context.AreaEspecializacao.Find(id);
        }

        public bool AreaExiste(string nome, int? areaIdAtual = null)
        {
            var consulta = _context.AreaEspecializacao.AsQueryable();

            if (areaIdAtual.HasValue)
            {
                consulta = consulta.Where(area => area.AreaID != areaIdAtual.Value);
            }

            return consulta.Any(a => a.Nome == nome);
        }

        public void Adicionar(AreaEspecializacao areaEspecializacao)
        {
            _context.AreaEspecializacao.Add(areaEspecializacao);
            _context.SaveChanges(); 
        }

        public void Atualizar(AreaEspecializacao area)
        {
            AreaEspecializacao areaBanco = _context.AreaEspecializacao.FirstOrDefault(a => a.AreaID == area.AreaID);

            if (areaBanco == null)
            {
                return;
            }

            areaBanco.Nome = area.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            AreaEspecializacao areaBanco = _context.AreaEspecializacao.FirstOrDefault(a => a.AreaID == id);

            if (areaBanco == null)
            {
                return;
            }

            _context.AreaEspecializacao.Remove(areaBanco);
            _context.SaveChanges();
        }
    }
}
