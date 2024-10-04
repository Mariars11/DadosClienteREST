using System;
using System.Collections.Generic;

namespace Apsen.Teste;

public partial class Telefone
{
    public int Id { get; set; }

    public string Ddd { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string CnpjCliente { get; set; } = null!;

    public virtual Cliente CnpjClienteNavigation { get; set; } = null!;
}
