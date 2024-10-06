using Apsen.Models;
using Apsen.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Apsen.Services;
public class ViaCepService : IViaCepService
{
    public async Task<Endereco> GetEnderecoViaCep(string cep){
        
        HttpClient client = new HttpClient();
        string urlViaCep = String.Format("https://viacep.com.br/ws/{0}/json/", cep);
        Console.WriteLine(urlViaCep);
        
        var clientGet = await client.GetAsync(urlViaCep);
        var contentGet = await clientGet.Content.ReadAsStringAsync();
        Console.WriteLine(contentGet);
        var jsonToken = JObject.Parse(contentGet);
        Endereco endereco = new(){
            Cep = cep,
            Logradouro = jsonToken.Value<string>("logradouro"),
            Complemento = jsonToken.Value<string>("complemento"),
            Bairro = jsonToken.Value<string>("bairro"),
            Estado = jsonToken.Value<string>("estado"),
            Cidade = jsonToken.Value<string>("localidade"),
        };

        return endereco;
        //Console.WriteLine(clientGet.Content);
    }
}