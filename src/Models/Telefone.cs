using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsen.Models;

public partial class Telefone
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe o DDD do telefone")]
    [RegularExpression(@"\d\d", ErrorMessage = "DDD inválido")]
    [MaxLength(2, ErrorMessage = "DDD com muitos dígitos")]
    [MinLength(2, ErrorMessage = "DDD com muitos dígitos")]

    public string Ddd { get; set; } = null!;
    [Required(ErrorMessage = "Informe o número celular")]
    [DataType(DataType.PhoneNumber)]
    [MaxLength(9, ErrorMessage = "Celular com quantidade de dígitos errado (9)")]
    [MinLength(9, ErrorMessage = "Celular com quantidade de dígitos errado (9)")]

    [RegularExpression(@"9\d\d\d\d\d\d\d+", ErrorMessage = "Celular inválido")]
    public string Celular { get; set; } = null!;
    [DataType(DataType.PhoneNumber)]
    [MaxLength(8, ErrorMessage = "Telefone com quantidade de dígitos errado (8)")]
    [MinLength(8, ErrorMessage = "Telefone com quantidade de dígitos errado (9)")]
    [RegularExpression(@"\d\d\d\d\d\d\d\d+", ErrorMessage = "Telefone inválido")]
    public string? TelefoneFixo { get; set; } 
    
    [Required(ErrorMessage = "Informe o CNPJ do cliente")]


    public int IdCliente { get; set; }

    public virtual Cliente? CnpjClienteNavigation { get; set; } 
}
