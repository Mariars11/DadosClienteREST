using System;
using System.Collections.Generic;

namespace Apsen;

public partial class Endereco
{
    public int Id { get; set; }

    public string Cep { get; set; } = null!;

    public string Endereco1 { get; set; } = null!;

    public string CpnjCliente { get; set; } = null!;

    public string Complemento { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public virtual Cliente? CpnjClienteNavigation { get; set; }
}
