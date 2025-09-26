# Sistema de Locadora de Filmes

Sistema web para administração de uma locadora de filmes desenvolvido em .NET Core 8 com Entity Framework Core e SQL Server.

## Arquitetura

- **3 Camadas**: Apresentação (Web), Serviços (Services) e Dados (Data + Repositories)
- **Backend**: ASP.NET Core MVC com Entity Framework Core
- **Frontend**: Interface simples em escala de cinza com formulários inline
- **Banco**: SQL Server em container Docker

## Entidades

- **Usuários**: Gerenciamento de usuários do sistema
- **Clientes**: Cadastro de clientes da locadora  
- **Filmes**: Catálogo de filmes disponíveis
- **Locações**: Controle de aluguéis

## Como Executar

### Pré-requisitos
- Docker e Docker Compose
- Portas 8080 e 1433 disponíveis

### Executar com Docker
```bash
docker-compose up -d
```

### Acessar Aplicação
```
http://localhost:8080
```

### Parar Aplicação
```bash
docker-compose down
```

## Funcionalidades

- ✅ Listagem de todos os dados em uma página única
- ✅ Adição inline de novos registros
- ✅ Interface minimalista em escala de cinza
- ✅ Dados de exemplo pré-carregados via seed
- ✅ Relacionamentos entre entidades funcionando

## Estrutura do Projeto

```
movie-store-vannon/
├── MovieStore.Web/      # Camada de Apresentação (MVC + Controllers)
├── MovieStore.Services/ # Camada de Serviços (Business Logic)
├── MovieStore.Data/     # Camada de Dados (Entities + DbContext + Repositories)
├── docker-compose.yml       # Orquestração Docker
├── Dockerfile              # Build da aplicação
└── README.md               # Documentação
```

## Tecnologias

- .NET Core 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server 2019
- Docker & Docker Compose


Developed by Eduardo Castro.