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
    }
}
