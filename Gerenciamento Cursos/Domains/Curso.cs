using System;
using System.Collections.Generic;

namespace Gerenciamento_Cursos.Domains;

public partial class Curso
{
    public int CursoID { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public int CargaHoraria { get; set; }

    public int InstrutorID { get; set; }

    public virtual Instrutor Instrutor { get; set; } = null!;

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();

    public virtual ICollection<Instrutor> Intrutor { get; set; } = new List<Instrutor>();
}
