using System;
using System.Collections.Generic;

namespace Gerenciamento_Cursos.Domains;

public partial class AreaEspecializacao
{
    public int AreaID { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<Instrutor> Instrutor { get; set; } = new List<Instrutor>();
}
