using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsen;

public partial class Telefone
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe o DDD do telefone")]
    [RegularExpression(@"\d\d", ErrorMessage = "DDD inválido")]
    public string Ddd { get; set; } = null!;
    [Required(ErrorMessage = "Informe o número de telefone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"9\d\d\d\d\d\d\d\d", ErrorMessage = "Telefone inválido")]
    public string Numero { get; set; } = null!;
    [Required(ErrorMessage = "Informe o CNPJ do cliente")]


    public string CnpjCliente { get; set; } = null!;

    public virtual Cliente? CnpjClienteNavigation { get; set; } 
}
