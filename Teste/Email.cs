using System;
using System.Collections.Generic;

namespace Apsen.Teste;

public partial class Email
{
    public int Id { get; set; }

    public string EnderecoEmail { get; set; } = null!;

    public string CnpjCliente { get; set; } = null!;

    public virtual Cliente CnpjClienteNavigation { get; set; } = null!;
}
