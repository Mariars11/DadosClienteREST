using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apsen.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ILogger<ClienteController> _logger;

    public ClienteController(ILogger<ClienteController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }
    private DataContext _context;

    //Listar clientes de forma paginada
    [HttpGet("PageSize={PageSize}&CurrentPage={CurrentPage}")]
    public IEnumerable<Cliente> GetClientes(int PageSize, int CurrentPage){
        Console.WriteLine(String.Format("Tamanho da pagina: {0}; Pagina atual: {1}", PageSize, CurrentPage));
        return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .Skip((CurrentPage - 1) * PageSize).Take(PageSize);
    }
    //Get de um cliente especifico
    [HttpGet("{cpnj}")]
    public Cliente GetCliente(string cpnj){
        return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .First(n => n.CNPJ == cpnj);
    }
    //Criar um ou varios clientes
    [HttpPost(Name = "PostClient")]
    public void CreateCliente(List<Cliente> cliente){
        _context.Clientes.AddRange(cliente);
        _context.SaveChanges();

    }
    //Atualizar um cliente
    [HttpPut("{cnpj}")]
    public void CreateCliente(string cnpj, Cliente cliente){
        _context.Entry(cliente).State = EntityState.Modified;
        _context.SaveChanges();
    }
    //Deletar um cliente
    [HttpDelete("{cnpj}")]
    public void DeleteCliente(string cnpj){
        Cliente clienteDeletar = _context.Clientes.Find(cnpj);
        _context.Clientes.Remove(clienteDeletar);
        _context.SaveChanges();
    }

    //Deletar todos os clientes
     [HttpDelete("")]
    public void DeleteAllClientes(){
        List<Cliente> clienteDeletar = _context.Clientes.ToList();
        _context.Clientes.RemoveRange(clienteDeletar);
        _context.SaveChanges();
    }
}
