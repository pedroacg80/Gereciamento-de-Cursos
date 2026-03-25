using System;
using System.Collections.Generic;

namespace Gerenciamento_Cursos.Domains;

public partial class Matricula
{
    public int CursoID { get; set; }

    public int AlunoID { get; set; }

    public int? NumeroMatricula { get; set; }

    public bool? StatusMatricula { get; set; }

    public virtual Aluno Aluno { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;
}
