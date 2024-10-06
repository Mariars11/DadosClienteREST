using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apsen.Models;

public partial class Email
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe um e-mail!")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
    [RegularExpression(@"^[a-zA-Z0-9._\\-]+@[a-zA-Z0-9]+(([\\-]*[a-zA-Z0-9]+)*[.][a-zA-Z0-9]+)+(;[ ]*[a-zA-Z0-9._\\-]+@[a-zA-Z0-9]+(([\\-]*[a-zA-Z0-9]+)*[.][a-zA-Z0-9]+)+)*$", ErrorMessage ="E-mail informado é inválido")]
    public string EnderecoEmail { get; set; } = null!;
    public int IdCliente { get; set; }

    public virtual Cliente? CnpjClienteNavigation { get; set; } 
}
