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

    
    [HttpGet(Name = "GetClientes")]
    public IEnumerable<Cliente> GetClientes(){
        return _context.Clientes;
    }
    [HttpGet("{cpnj}")]
    public Cliente GetCliente(string cpnj){
        return _context.Clientes.First(n => n.CNPJ == cpnj);
    }
    [HttpPost(Name = "PostClient")]
    public void CreateCliente(Cliente cliente){
        _context.Clientes.Add(cliente);
        _context.SaveChanges();

    }
    [HttpPut("{cnpj}")]
    public void CreateCliente(string cnpj, Cliente cliente){
        _context.Entry(cliente).State = EntityState.Modified;
        _context.SaveChanges();
    }
    [HttpDelete("{cnpj}")]
    public void DeleteCliente(string cnpj){
        Cliente clienteDeletar = _context.Clientes.Find(cnpj);
        _context.Clientes.Remove(clienteDeletar);
        _context.SaveChanges();
    }
}
