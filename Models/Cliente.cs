namespace Apsen;

public class Cliente
{
    public string CNPJ { get; set; }

    public string Nome { get; set; }

    public bool Flag_Status {get; set;}

    public List<string> ListEnderecoes { get; set;}
    public List<string> ListTelefones { get; set;}
    public List<string> ListEmails { get; set;}

}
