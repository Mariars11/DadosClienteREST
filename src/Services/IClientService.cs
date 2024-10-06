
using Apsen.Models;

namespace Apsen.Services;
public interface IClientService
{
    public IEnumerable<Cliente> GetClientes(int PageSize, int CurrentPage);
    public IEnumerable<Cliente> GetClientesAtividade(int PageSize, int CurrentPage, bool IsAtivo);
    public Cliente GetCliente(string cpnj);
    public string CreateCliente(List<Cliente> clientes);
    public string UpdateCliente(int ID, Cliente cliente);
    public string DeleteCliente(string cnpj);
    public string DeleteAllClientes();
    public Endereco BuscarEndereco(string cep);

    
}