using Apsen.Models;

namespace Apsen.Services;
public interface IViaCepService
{
    public Task<Endereco> GetEnderecoViaCep(string cep);
}
