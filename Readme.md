# Projeto de Cadastro de Clientes Blazor

Esse projeto foi desenvolvido em .net 8.0 o Blazor no frontend

As principais tecnologias e conceitos utilizados foram:

 - .net 8.0
 - Blazor
 - Migrations
 - Extension Methods
 - Explicit Conversions
 - fluent Validation
 - Mysql

## Configurar Projeto

### `appsettings.json`

- atualizar a `ConnectionStrings:DefaultConnection`  para um banco Mysql

### `Migrations`

para subir o banco é nescessário roda o comando `dotnet ef database update` na pasta `Clientes.Api`

### `Script`

segue `query` caso queria criar a tabela manualmente:

```sql

CREATE DATABASE `clientsDb`;

CREATE TABLE `Clientes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NomeRazao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Telefone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TipoPessoa` int NOT NULL,
  `CpfCnpj` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `InscricaoEstadual` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `InscricaoEstadualPessoaFisica` tinyint(1) NOT NULL,
  `Isento` tinyint(1) NOT NULL,
  `Genero` int DEFAULT NULL,
  `DataNascimento` datetime(6) DEFAULT NULL,
  `Bloqueado` tinyint(1) NOT NULL,
  `Senha` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DataCadastro` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```
