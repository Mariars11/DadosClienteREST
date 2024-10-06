using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsen.Models;

public partial class Cliente
{
    public int ID { get; set; }
    [Required(ErrorMessage = "Informe o valor do CNPJ!")]
    [MinLength(14, ErrorMessage = "Quantidade de dígitos inválida! (14)")]
    [MaxLength(14, ErrorMessage = "Quantidade de dígitos inválida! (14)")]
    public string CNPJ { get; set; } = null!;
    [Required(ErrorMessage = "Informe o nome!")]
    [MinLength(2, ErrorMessage = "Nome inválido")]
    public string Nome { get; set; } = null!;
    [Required(ErrorMessage = "Informe um status!")]
    public bool FlagStatusAtivo { get; set; }

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();

    public virtual ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
}
