using Gerenciamento_Cursos.Domains;

namespace Gerenciamento_Cursos.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();
        Instrutor? ObterPorId(int id);   
        Instrutor? ObterPorEmail(string email);
        bool EmailExiste(string email); 
        void Adicionar (Instrutor instrutor);
        void Atualizar(Instrutor instrutor);
        void Remover(int id);
    }
}
