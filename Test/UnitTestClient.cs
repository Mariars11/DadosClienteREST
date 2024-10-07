
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
                            TelefoneFixo = "29348255",
                            Ddd = "61"
                        }
                    }
                },
                new Cliente{
                    Nome = "Vanessa",
                    CNPJ = "31949769747789",
                    FlagStatusAtivo = true,
                    Emails = new List<Email>{
                        new Email {
                            EnderecoEmail = "vanessa_drumond@zian.com.br"
                        }
                    },
                    Enderecos = new List<Endereco>{
                        new Endereco {
                            Cep = "94470410",
                            Logradouro = "Rua Principal",
                            Numero = "221",
                            Bairro = "Centro",
                            Cidade = "Santa Elvira",
                            Estado = "MT"
                        }
                    },
                    Telefones = new List<Telefone>{
                        new Telefone {
                            Celular = "983962396",
                            TelefoneFixo = "29348255",
                            Ddd = "66"
                        }
                    }
                }
            };
            
            return clientes;
        }
        /// <summary>
        /// Testa a adição de um cliente sucesso
        /// </summary>
        [Fact]
        public void AddCliente_Cliente()
        {
            //arrange
            string response = "Cliente(s) criados";
            var clientList = GetClientesData();
            clientServices.Setup(x => x.CreateCliente(clientList))
                .Returns(response);
            var clientController = new ClienteController(clientServices.Object);
            //act
            var clientResult = clientController.CreateCliente(clientList).Result;
            var resultType = clientResult as OkObjectResult;
            
            Console.WriteLine(clientResult);
            //assert
            Assert.NotNull(clientResult);

            Assert.Equal(response, resultType.Value.ToString());
        }
         /// <summary>
        /// Testa a adição de um cliente erro
        /// </summary>
        [Fact]
        public void AddClienteJaExistente_Cliente()
        {
            string cnpj = "12345678912345";
            //arrange
            string response = String.Format("Bad request 400 - O CNPJ {0} já existe na base de dados", cnpj);
            var clientList = GetClientesData();
            clientServices.Setup(x => x.CreateCliente(clientList))
                .Returns(response);
            var clientController = new ClienteController(clientServices.Object);
            //act
            var clientResult = clientController.CreateCliente(clientList).Result;
            var resultType = clientResult as BadRequestObjectResult;
            
            Console.WriteLine(clientResult);
            //assert
            Assert.NotNull(clientResult);

            Assert.Equal(response, resultType.Value.ToString());
        }
        /// <summary>
        /// Testa o get dos clientes com paginação
        /// </summary>
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
            var clientsResult = clientController.GetClientes(PageSize, CurrentPage);
            //assert
            Assert.NotNull(clientsResult);
            Assert.Equal(GetClientesData().Count(), clientsResult.Count());
            Assert.Equal(GetClientesData().ToString(), clientsResult.ToString());
            Assert.True(clientList.Equals(clientsResult));
        }
         /// <summary>
        /// Testa o get de um cliente especifico
        /// </summary>
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
            var result = clientController.GetCliente(cnpj).Result.Result;
            var resultType = result as OkObjectResult;
            var clientResult = (Cliente)resultType.Value;
            
            //assert
            Assert.NotNull(clientResult);
            Assert.Equal(clientList[0].ID, clientResult.ID);
            Assert.True(clientList[0].ID == clientResult.ID);
        }
         /// <summary>
        /// Testa edição cliente sucesso
        /// </summary>
         [Fact]
        public void UpdateClienteByID_Cliente()
        {
            Cliente cliente = new(){
                    Nome = "Maria R",
                    CNPJ = "12345678912345",
                    FlagStatusAtivo = true,
                    Emails = new List<Email>{
                        new Email {
                            EnderecoEmail = "mariaclara@gmail.com"
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
                            TelefoneFixo = "29348255",
                            Ddd = "61"
                        }
                    }
                };
            //arrange
            int id = 1;
            string response = String.Format("Cliente com CPNJ: {0} e ID: {1} atualizado com sucesso!", cliente.CNPJ, id.ToString());
            var clientList = GetClientesData();
            clientServices.Setup(x => x.UpdateCliente(id, cliente))
                .Returns(response);
            var clientController = new ClienteController(clientServices.Object);
            //act
            var clientResult = clientController.UpdateCliente(id, cliente).Result;
            var resultType = clientResult as OkObjectResult;
            
            //assert
            Assert.NotNull(clientResult);

            Assert.Equal(response, resultType.Value.ToString());
        }
        /// <summary>
        /// Testa exclusão de um cliente
        /// </summary>
         [Fact]
        public void DeleteClienteByCNPJ_Cliente()
        {
            //arrange
            string cpnj = "31949769747789";
            string response = "Cliente excluído com sucesso!";
            var clientList = GetClientesData();
            clientServices.Setup(x => x.DeleteCliente(cpnj))
                .Returns(response);
            var clientController = new ClienteController(clientServices.Object);
            //act
            var clientResult = clientController.DeleteCliente(cpnj).Result;
            var resultType = clientResult as OkObjectResult;
            
            //assert
            Assert.NotNull(clientResult);

            Assert.Equal(response, resultType.Value.ToString());
        }
    }
}