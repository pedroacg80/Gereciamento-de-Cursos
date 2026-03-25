namespace Gerenciamento_Cursos.DTOs.Instrutor
{
    public class CriarInstrutorDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[]? Senha { get; set; }
        public int? AreaEspecializacaoID { get; set; }


    }
}
