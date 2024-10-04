using System;
using System.Collections.Generic;

namespace Apsen;

public partial class Cliente
{
    public string CNPJ { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Sobrenome { get; set; } = null!;

    public bool FlagStatusAtivo { get; set; }

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();

    public virtual ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
}
