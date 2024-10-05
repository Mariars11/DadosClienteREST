using Apsen.Services;
using Apsen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apsen.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClientService _clienteService;
    public ClienteController(IClientService clienteService)
    {
        _clienteService = clienteService;
    }
    /// <summary>
        /// Listar clientes de forma paginada
    /// </summary>
    ///<param name="PageSize" example="10"></param>
    ///<param name="CurrentPage" example="1"></param>

    [HttpGet("PageSize={PageSize}&CurrentPage={CurrentPage}")]
    public IEnumerable<Cliente> GetClientes(int PageSize, int CurrentPage){
        Console.WriteLine(String.Format("Tamanho da pagina: {0}; Pagina atual: {1}", PageSize, CurrentPage));
        return _clienteService.GetClientes(PageSize, CurrentPage);
    }

    /// <summary>
        /// Listar clientes com base no status e paginação
    /// </summary>
    
    ///<param name="PageSize" example="10"></param>
    ///<param name="CurrentPage" example="1"></param>
    ///<param name="IsAtivo" example="true"></param>

    [HttpGet("$PageSize={PageSize}&CurrentPage={CurrentPage}&IsAtivo={IsAtivo}")]
    public IEnumerable<Cliente> GetClientesAtividade(int PageSize, int CurrentPage, bool IsAtivo){
        return _clienteService.GetClientesAtividade(PageSize, CurrentPage, IsAtivo);
    }

    /// <summary>
        /// Retorna um cliente especifico
    /// </summary>

    ///<param name="cpnj" example="12345678912345"></param>
    [HttpGet("{cpnj}")]
    public async Task<ActionResult<Cliente>> GetCliente(string cpnj){
            var cliente = _clienteService.GetCliente(cpnj);
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
        var response = _clienteService.CreateCliente(clientes);

        if(response.Contains("Bad request 400")){
            return BadRequest(response);
        }
        else if(response.Contains("OK 200")){
             return Ok("Cliente(s) criados!");
        }   
        return Ok(response);    
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
        var response = _clienteService.UpdateCliente(ID, cliente);

        if(response.Contains("Bad Request 400")){
            return BadRequest(response);
        }
        else if(response.Contains("OK 200")){
            return Ok(String.Format("Cliente com CPNJ: {0} e ID: {1} atualizado com sucesso!", cliente.CNPJ, ID));
        }
        return Ok(response);
        
    }
    /// <summary>
    /// Deleta um cliente 
    /// </summary>
    ///<param name="cnpj" example="12345678912345"></param>
    [HttpDelete("{cnpj}")]
    public async Task<IActionResult> DeleteCliente(string cnpj){
        string response = _clienteService.DeleteCliente(cnpj);
        
        if(response.Contains("Not found 404")){
            return NotFound(String.Format("CNPJ {0} não existe!", cnpj));
        }
        else if(response.Contains("OK 200")){
            return Ok("Cliente excluído com sucesso!");        
        }
        else if(response.Contains("Bad Request 400")){
            return BadRequest(response);
        }
        return Ok(response);
    }
    /// <summary>
    /// Deleta todos os clientes registrados
    /// </summary>
    /// <response code="200">Todos clientes excluídos</response>
    /// <response code="400">Erro ao excluir clientes</response>
     [HttpDelete("")]
    public async Task<IActionResult> DeleteAllClientes(){
        string response = _clienteService.DeleteAllClientes();
        
        if(response.Contains("OK 200")){
            return Ok("Clientes excluídos com sucesso!");        
        }
        else if(response.Contains("Bad Request 400")){
            return BadRequest(response);
        }
        return Ok(response);
    }
}
