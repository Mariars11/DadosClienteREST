using Apsen.Models;
using Apsen.Services;
using Microsoft.EntityFrameworkCore;

namespace Apsen.Services;
public class ClientService : IClientService
{
    private readonly ILogger<ClientService> _logger;
    private readonly DataContext _context;
    private readonly IViaCepService _viaCepService;

    public ClientService(ILogger<ClientService> logger, DataContext context, IViaCepService viaCepService)
    {
        _logger = logger;
        _context = context;
        _viaCepService = viaCepService;
    }
    public IEnumerable<Cliente> GetClientes(int PageSize, int CurrentPage){
            return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .Skip((CurrentPage - 1) * PageSize).Take(PageSize);
    }

    public IEnumerable<Cliente> GetClientesAtividade(int PageSize, int CurrentPage, bool IsAtivo){
            return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .Skip((CurrentPage - 1) * PageSize).Take(PageSize)
                        .Where(n => n.FlagStatusAtivo == IsAtivo);
    }
    public Cliente GetCliente(string cpnj){
            var cliente = _context.Clientes
                            .Include(n => n.Telefones)
                            .Include(n => n.Enderecos)
                            .Include(n => n.Emails)
                            .FirstOrDefault(n => n.CNPJ == cpnj);
            return cliente;
    }
    public string CreateCliente(List<Cliente> clientes){
        string responseCode = string.Empty;
        foreach (var cliente in clientes)
        {
            _viaCepService.GetEnderecoViaCep(cliente.Enderecos.First().Cep);
            var findClient = _context.Clientes.Where(n => n.CNPJ == cliente.CNPJ);
            if(!findClient.Any()){
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            else{
                responseCode = String.Format("Bad request 400 - O CNPJ {0} já existe na base de dados", cliente.CNPJ);
            }
        }  
        responseCode = "OK 200";

        return responseCode;      
    }
    public string UpdateCliente(int ID, Cliente cliente){
        string responseMessage = string.Empty; 
        _context.Entry(cliente).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
            responseMessage = "OK 200";

        }
        catch (Exception ex)
        {
            responseMessage = String.Format("Bad Request 400 - {0}", ex.Message);
        }
        return responseMessage;
    }

    public string DeleteCliente(string cnpj){
        string responseCode = string.Empty;
        Cliente clienteDeletar = _context.Clientes.FirstOrDefault( n => n.CNPJ == cnpj);
        if(clienteDeletar == null){
            responseCode = "Not found 404";
        }
        else{   
            try
            {
                _context.Clientes.Remove(clienteDeletar);
                _context.SaveChanges();

                responseCode = "OK 200";

            }
            catch (Exception ex)
            {
                responseCode = String.Format("Bad Request 400 - {0}", ex.Message);
            }
        }
        return responseCode;
    }
     public string DeleteAllClientes(){
        string responseCode = string.Empty;
        List<Cliente> clienteDeletar = _context.Clientes.ToList();

        try
        {
            _context.Clientes.RemoveRange(clienteDeletar);
            _context.SaveChanges();

            responseCode = "OK 200";

        }
        catch (Exception ex)
        {
            responseCode = String.Format("Bad Request 400 - {0}", ex.Message);
        }

        return responseCode;
    }
}