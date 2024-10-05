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
    /// <summary>
    /// Listar clientes de forma paginada
    /// </summary>
    ///<param name="PageSize" example="10"></param>
    ///<param name="CurrentPage" example="1"></param>
    [HttpGet("PageSize={PageSize}&CurrentPage={CurrentPage}")]
    public IEnumerable<Cliente> GetClientes(int PageSize, int CurrentPage){
        Console.WriteLine(String.Format("Tamanho da pagina: {0}; Pagina atual: {1}", PageSize, CurrentPage));
        return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .Skip((CurrentPage - 1) * PageSize).Take(PageSize);
    }
    /// <summary>
    /// Retorna um cliente especifico
    /// </summary>
    ///<param name="cpnj" example="12345678912345"></param>
    [HttpGet("{cpnj}")]
    public Cliente GetCliente(string cpnj){
        return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .First(n => n.CNPJ == cpnj);
    }
/// <summary>
/// Cria um ou vários clientes
/// </summary>
/// <returns>Um novo cliente</returns>
/// <remarks>
/// Sample request:
///
///     POST /Cliente
///     [
///         {
///             "cnpj": "12345678912345",
///             "nome": "Maria Clara",
///             "sobrenome": "Silva",
///             "flagStatusAtivo": true,
///             "emails": [
///                 {
///                     "id": 0,
///                     "enderecoEmail": "mariaclara@gmail.com",
///                     "cnpjCliente": "12345678912345"
///                 }
///             ],
///             "enderecos": [
///                 {
///                     "id": 0,
///                     "cep": "94470410",
///                     "logradouro": "Travessa Inácio de Fraga",
///                     "complemento": "",
///                     "numero": "810",
///                     "bairro": "Viamópolis",
///                     "cidade": "Viamão",
///                     "estado": "RS",
///                     "cpnjCliente": "12345678912345"
///                 }
///             ],
///             "telefones": [
///                 {
///                     "id": 0,
///                     "ddd": "51",
///                     "celular": "983962396",
///                     "telefoneFixo": "29348255",
///                     "cnpjCliente": "12345678912345"
///                 }
///             ]
///         }
///     ]
///
/// </remarks>
/// <response code="200">Cliente criado com sucesso</response>
/// <response code="400">Inconformidade com algum campo</response>
    [HttpPost(Name = "PostClient")]
    public void CreateCliente(List<Cliente> cliente){
        _context.Clientes.AddRange(cliente);
        _context.SaveChanges();

    }
/// <summary>
/// Atualiza um ou varios campos de um cliente
/// </summary>
/// <returns>Cliente Atualizado</returns>
/// <remarks>
/// Sample request:
///
///     PUT /Cliente
///         {
///             "cnpj": "12345678912345",
///             "nome": "Maria Clara",
///             "sobrenome": "R Silva",
///             "flagStatusAtivo": true,
///             "emails": [
///                 {
///                     "id": 0,
///                     "enderecoEmail": "maria@gmail.com",
///                     "cnpjCliente": "12345678912345"
///                 }
///             ],
///             "enderecos": [
///                 {
///                     "id": 0,
///                     "cep": "94470410",
///                     "logradouro": "Travessa Inácio de Fraga",
///                     "complemento": "",
///                     "numero": "810",
///                     "bairro": "Viamópolis",
///                     "cidade": "Viamão",
///                     "estado": "RS",
///                     "cpnjCliente": "12345678912345"
///                 }
///             ],
///             "telefones": [
///                 {
///                     "id": 0,
///                     "ddd": "61",
///                     "celular": "983962396",
///                     "telefoneFixo": "29348255",
///                     "cnpjCliente": "12345678912345"
///                 }
///             ]
///         }
///
/// </remarks>
/// <response code="200">Cliente atualizado com sucesso</response>
/// <response code="400">Inconformidade com algum campo</response>
///     ///<param name="cnpj" example="12345678912345"></param>

    //Atualizar um cliente
    [HttpPut("{cnpj}")]
    public void CreateCliente(string cnpj, Cliente cliente){
        _context.Entry(cliente).State = EntityState.Modified;
        _context.SaveChanges();
    }
    /// <summary>
    /// Deleta um cliente 
    /// </summary>
    ///<param name="cnpj" example="12345678912345"></param>
    [HttpDelete("{cnpj}")]
    public void DeleteCliente(string cnpj){
        Cliente clienteDeletar = _context.Clientes.Find(cnpj);
        _context.Clientes.Remove(clienteDeletar);
        _context.SaveChanges();
    }
    /// <summary>
    /// Deleta todos os clientes registrados
    /// </summary>
    
     [HttpDelete("")]
    public void DeleteAllClientes(){
        List<Cliente> clienteDeletar = _context.Clientes.ToList();
        _context.Clientes.RemoveRange(clienteDeletar);
        _context.SaveChanges();
    }
}
