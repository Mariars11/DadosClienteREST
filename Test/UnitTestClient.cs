
using Apsen.Controllers;
using Apsen.Models;
using Apsen.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestClient
{
    public class UnitTestClient
    {
        private readonly Mock<IClientService> clientServices;
        public UnitTestClient()
        {
            clientServices = new Mock<IClientService>();
        }
        public List<Cliente> GetClientesData()
        {
            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente{
                    Nome = "Maria Clara",
                    CNPJ = "12345678912345",
                    Sobrenome = "Silva",
                    FlagStatusAtivo = true,
                    Emails = new List<Email>{
                        new Email {
                            EnderecoEmail = "maria@gmail.com"
                        }
                    },
                    Enderecos = new List<Endereco>{
                        new Endereco {
                            Cep = "94470410",
                            Logradouro = "Travessa Inácio de Fraga",
                            Numero = "810",
                            Bairro = "Viamópolis",
                            Cidade = "Viamão",
                            Estado = "RS"
                        }
                    },
                    Telefones = new List<Telefone>{
                        new Telefone {
                            Celular = "983962396",
                            TelefoneFixo = "29348255"
                        }
                    }
                }
            };
            
            return clientes;
        }
        [Fact]
        public void GetClientesList_Clientes()
        {
            int PageSize = 10;
            int CurrentPage = 1;
            //arrange
            var clientList = GetClientesData();
            clientServices.Setup(x => x.GetClientes(PageSize, CurrentPage))
                .Returns(clientList);
            var clientController = new ClienteController(clientServices.Object);
            //act
            var clientsResult = clientController.GetClientes(PageSize, CurrentPage);;
            //assert
            Assert.NotNull(clientsResult);
            Assert.Equal(GetClientesData().Count(), clientsResult.Count());
            Assert.Equal(GetClientesData().ToString(), clientsResult.ToString());
            Assert.True(clientList.Equals(clientsResult));
        }

        [Fact]
        public void GetClientByCNPJ_Cliente()
        {
            string cnpj = "12345678912345";
            //arrange
            var clientList = GetClientesData();
            clientServices.Setup(x => x.GetCliente(cnpj))
                .Returns(clientList[0]);
            var clientController = new ClienteController(clientServices.Object);
            //act
            var clientResult = clientController.GetCliente(cnpj).Result.Value;
            //assert
            Assert.NotNull(clientResult);
            Assert.Equal(clientList[0].ID, clientResult.ID);
            Assert.True(clientList[0].ID == clientResult.ID);
        }

        [Fact]
        public void AddCliente_Cliente()
        {
            //arrange
            string response = "OK 200";
            var clientList = GetClientesData();
            clientServices.Setup(x => x.CreateCliente(clientList))
                .Returns(response);
            var productController = new ClienteController(clientServices.Object);
            //act
            var productResult = productController.CreateCliente(clientList);
            //assert
            Assert.NotNull(productResult);
        }
    }
}