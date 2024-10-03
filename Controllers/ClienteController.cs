using Microsoft.AspNetCore.Mvc;

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

    [HttpPost(Name = "PostClient")]
    public void CreateCliente(Cliente cliente){
        _context.Clientes.Add(cliente);
        _context.SaveChanges();

    }
}
