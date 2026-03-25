using Gerenciamento_Cursos.Contexts;
using Gerenciamento_Cursos.Domains;
using Gerenciamento_Cursos.Interfaces;

namespace Gerenciamento_Cursos.Repositories
{
    public class InstrutorRepository : IInstrutorRepository
    {
        private readonly GerenciamentoCursosContext _context;

        public InstrutorRepository(GerenciamentoCursosContext context)
        {
            _context = context;
        }

        public List<Instrutor> Listar()
        {
            return _context.Instrutor.ToList();
        }

        public Instrutor? ObterPorId(int id)
        {
            return _context.Instrutor.Find(id);
        }

        public Instrutor? ObterPorEmail(string email)
        {
            return _context.Instrutor.FirstOrDefault(i => i.Email == email);
        }

        public bool EmailExiste(string email)
        {
            return _context.Instrutor.Any(i => i.Email == email);
        }

        public void Adicionar(Instrutor instrutor)
        {
            _context.Instrutor.Add(instrutor);
            _context.SaveChanges();
        }

        public void Atualizar(Instrutor instrutor)
        {
            Instrutor? instrutorBanco = _context.Instrutor.FirstOrDefault(i => i.InstrutorID == instrutor.InstrutorID);

            if (instrutorBanco == null)
            {
                return;
            }

            instrutorBanco.Nome = instrutor.Nome;
            instrutorBanco.Email = instrutor.Email;
            instrutorBanco.Senha = instrutor.Senha;
            instrutorBanco.AreaEspecializacaoID = instrutor.AreaEspecializacaoID;

            _context.SaveChanges();
        }


        public void Remover(int id)
        {
            Instrutor? instrutor = _context.Instrutor.FirstOrDefault(i => i.InstrutorID == id);

            if (instrutor == null)
            {
                return;
            }

            _context.Instrutor.Remove(instrutor);
            _context.SaveChanges();
        }
    }
}
