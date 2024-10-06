# CRUD APIs RESTful - Cliente

# Rodar projeto
- <b style="color:red">Necessário sdk .net 8 </b> <br>
> git clone https://github.com/Mariars11/DadosClienteREST <br>
## Rodar api
> cd ./src <br>
> dotnet restore <br>
> dotnet build <br>
> dotnet watch run <br>
## Rodar teste
> cd ../test <br>
> dotnet test <br>

# Utilização da API

## GET

> GET Paginado
```
    http://localhost:5052/Cliente/PageSize={PageSize}&CurrentPage={CurrentPage}
```
- Retorna os clientes persistidos, de forma paginada.
- Parametros:
    - PageSize: Quantidade de clientes;
    - CurrentPage: Pagina desejada
- Ex.: http://localhost:5052/Cliente/PageSize=10&CurrentPage=1

> GET Paginado e status
```
    http://localhost:5052/Cliente/$PageSize={PageSize}&CurrentPage={CurrentPage}&IsAtivo={IsAtivo}
```
- Retorna os clientes persistidos, de forma paginada.
- Parametros:
    - PageSize: Quantidade de clientes;
    - CurrentPage: Pagina desejada;
    - IsAtivo: true/false
- Ex.: http://localhost:5052/Cliente/PageSize=10&CurrentPage=1&IsAtivo=false

> GET Unitario
```
    http://localhost:5052/Cliente/{CNPJ}
```
- Retorna um unico cliente com base no cnpj
- Parametros:
    - CNPJ: cnpj do cliente;
- Ex.: http://localhost:5052/Cliente/12345678912345

> GET Endereço

```
    http://localhost:5052/Endereco/{cep}
```

- Retorna um endereço com base no cep através da api viacep 
- Parametros:
    - CEP
- Ex.: http://localhost:5052/Endereco/94470410
## POST
> Criação
```
    http://localhost:5052/Cliente
```
- Adiciona um ou mais clientes.
- Body:
    - JSON
- Exemplo no arquivo: Clientes.json

## PUT
> Edição
```
    http://localhost:5052/Cliente/{CNPJ}
```
- Edita um único cliente
- Parametros:
    - CNPJ: cnpj do cliente;
- Ex.: http://localhost:5052/Cliente/12345678912345

## DELETE
> Exclusão Unitária
```
    http://localhost:5052/Cliente/{CNPJ}
```
- Deleta um único cliente
- Parametros:
    - CNPJ: cnpj do cliente;
- Ex.: http://localhost:5052/Cliente/12345678912345
> Exclusão total
```
    http://localhost:5052/Cliente
```
- Deleta todos os registros
- Ex.: http://localhost:5052/Cliente

# Arquitetura MVC
## Models
- Cliente
    - ID
    - CNPJ (string, 14 digitos)
    - Nome cliente (string)
    - Status (bool, ativo/inativo)

- Email
    - ID (key)
    - Endereço (Mascara email: maria@gmail.com)
    - ID_Cliente (relacionamento com cliente)

- Telefone
    - ID (key)
    - DDD (Mascara email: xx (dois digitos))
    - Celular (9xxxxxxxx)
    - Telefone_Fixo (xxxxxxxx)
    - ID_Cliente (relacionamento com cliente)

- Endereco
    - ID (key)
    - CEP (8 digitos)
    - Logradouro 
    - Complemento
    - Bairro
    - Cidade
    - Estado
    - Numero
    - ID_Cliente (relacionamento com cliente)





