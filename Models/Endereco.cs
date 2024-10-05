using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsen;

public partial class Endereco
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe o cep")]
    [MinLength(8, ErrorMessage = "Quantidade de dígitos inválida! (8)")]
    [MaxLength(8, ErrorMessage = "Quantidade de dígitos inválida! (8)")]
    public string Cep { get; set; } = null!;
    [Required(ErrorMessage = "Informe o endereço")]
    public string Logradouro { get; set; } = null!;
    public string? Complemento { get; set; }
    [Required(ErrorMessage = "Informe o bairro")]
    [MaxLength(6, ErrorMessage = "Número de endereço muito grande")]
    [RegularExpression(@"\d+", ErrorMessage = "Valor não é um número")]
    public string Numero { get; set; } = null!;
    
    [Required(ErrorMessage = "Informe o bairro")]
    public string Bairro { get; set; } = null!;
    [Required(ErrorMessage = "Informe a cidade")]
    public string Cidade { get; set; } = null!;
    [Required(ErrorMessage = "Informe o estado")]
    public string Estado { get; set; } = null!;
    
    public int IdCliente { get; set; } 
    public virtual Cliente? CpnjClienteNavigation { get; set; }
}
