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
        /// Listar clientes com base no status e paginação
    /// </summary>
    
    ///<param name="PageSize" example="10"></param>
    ///<param name="CurrentPage" example="1"></param>
    ///<param name="IsAtivo" example="true"></param>

    [HttpGet("$PageSize={PageSize}&CurrentPage={CurrentPage}&IsAtivo={IsAtivo}")]
    public IEnumerable<Cliente> GetClientesAtividade(int PageSize, int CurrentPage, bool IsAtivo){
        return _context.Clientes
                        .Include(n => n.Telefones)
                        .Include(n => n.Enderecos)
                        .Include(n => n.Emails)
                        .Skip((CurrentPage - 1) * PageSize).Take(PageSize)
                        .Where(n => n.FlagStatusAtivo == IsAtivo);
    }

    /// <summary>
        /// Retorna um cliente especifico
    /// </summary>

    ///<param name="cpnj" example="12345678912345"></param>
    [HttpGet("{cpnj}")]
    public async Task<ActionResult<Cliente>> GetCliente(string cpnj){
            var cliente = _context.Clientes
                            .Include(n => n.Telefones)
                            .Include(n => n.Enderecos)
                            .Include(n => n.Emails)
                            .FirstOrDefault(n => n.CNPJ == cpnj);
            if(cliente == null){
                return NotFound();
            }
            else{
                return Ok(cliente);
            }
    }
/// <summary>
    /// Cria um ou vários clientes
/// </summary>
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
///                     "idCliente": 0,
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
///                     "idCliente": 0,
///                 }
///             ],
///             "telefones": [
///                 {
///                     "id": 0,
///                     "ddd": "51",
///                     "celular": "983962396",
///                     "telefoneFixo": "29348255",
///                     "idCliente": 0,
///                 }
///             ]
///         }
///     ]
///
/// </remarks>
/// <response code="200">Cliente criado com sucesso</response>
/// <response code="400">Inconformidade com algum campo</response>
    [HttpPost(Name = "PostClient")]
    public async Task<IActionResult> CreateCliente(List<Cliente> clientes){
        foreach (var cliente in clientes)
        {
            var findClient = _context.Clientes.Where(n => n.CNPJ == cliente.CNPJ);
            if(!findClient.Any()){
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            else{
                return BadRequest(String.Format("O CNPJ {0} já existe na base de dados", cliente.CNPJ));
            }
        }
        return Ok("Cliente(s) criados!");
        
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
///             "id": "1",
///             "cnpj": "12345678912345",
///             "nome": "Maria Clara",
///             "sobrenome": "R Silva",
///             "flagStatusAtivo": true,
///             "emails": [
///                 {
///                     "id": 0,
///                     "enderecoEmail": "maria@gmail.com",
///                     "idCliente": 1,
///                 }
///             ],
///             "enderecos": [
///                 {
///                     "id": 0,
///                     "cep": "94470411",
///                     "logradouro": "Travessa Fraga",
///                     "complemento": "",
///                     "numero": "810",
///                     "bairro": "Viamópolis",
///                     "cidade": "Viamão",
///                     "estado": "RS",
///                     "idCliente": 1,
///                 }
///             ],
///             "telefones": [
///                 {
///                     "id": 0,
///                     "ddd": "61",
///                     "celular": "983962396",
///                     "telefoneFixo": "29348255",
///                     "idCliente": 1,
///                 }
///             ]
///         }
///
/// </remarks>
/// <response code="200">Cliente atualizado com sucesso</response>
/// <response code="400">Inconformidade com algum campo</response>
///     ///<param name="ID" example="1"></param>

    //Atualizar um cliente
    [HttpPut("{ID}")]
    public async Task<IActionResult> UpdateCliente(int ID, Cliente cliente){
        _context.Entry(cliente).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();

            return Ok(String.Format("Cliente com CPNJ: {0} e ID: {1} atualizado com sucesso!", cliente.CNPJ, ID));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Deleta um cliente 
    /// </summary>
    ///<param name="cnpj" example="12345678912345"></param>
    [HttpDelete("{cnpj}")]
    public async Task<IActionResult> DeleteCliente(string cnpj){
        Cliente clienteDeletar = _context.Clientes.Find(cnpj);
        try
        {
            _context.Clientes.Remove(clienteDeletar);
            _context.SaveChanges();

            return Ok("Clientes excluídos com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Deleta todos os clientes registrados
    /// </summary>
    /// <response code="200">Todos clientes excluídos</response>
    /// <response code="400">Erro ao excluir clientes</response>
     [HttpDelete("")]
    public async Task<IActionResult> DeleteAllClientes(){
        List<Cliente> clienteDeletar = _context.Clientes.ToList();

        try
        {
            _context.Clientes.RemoveRange(clienteDeletar);
            _context.SaveChanges();

            return Ok("Clientes excluídos com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
