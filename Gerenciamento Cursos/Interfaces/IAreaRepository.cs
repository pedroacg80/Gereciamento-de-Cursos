using Gerenciamento_Cursos.Domains;

namespace Gerenciamento_Cursos.Interfaces
{
    public interface IAreaRepository
    {
        List<AreaEspecializacao> Listar();
        AreaEspecializacao? ObterPorId(int id);
        bool AreaExiste(string nome, int? areaIdAtual = null);
        void Adicionar(AreaEspecializacao area);
        void Atualizar(AreaEspecializacao area);
        void Remover(int id);
    }
}
