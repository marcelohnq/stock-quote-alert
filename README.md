# Stock Quote Alert

Um aplicação para desktop para monitorar se a cotação de uma ação da B3 está com tendência de alta ou queda.

## Dependências

- EF Core - Migrations
- Docker & [Testcontainers.MsSql](https://dotnet.testcontainers.org/modules/mssql/)

## Configurações

Realizar a configuração do programa da seguinte forma:

```
//src/StockQuote.Console/appsettings.json

  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "APIQuote": {
    "URLEndpoint": "https://query1.finance.yahoo.com/v8/finance/chart/",
    "DefaultRequestTime": "60000"
  },
  "Email": {
    "SmtpHost": "",
    "SmtpPort": "",
    "SmtpUser": "",
    "SmtpToken": "",
    "To": ""
  }
```

- ConnectionStrings:DefaultConnection - Definir o endereço para o banco de dados SQL Server
- APIQuote:URLEndpoint - Endereço para requisição da cotação do ativo
- APIQuote:DefaultRequestTime - Tempo de intervalo para cada requisição da cotação (padrão de 1 minuto)
- Email - Configuração para o servidor SMTP e o usuário deste servidor

## Funcionalidades

O sistema possui as seguintes funcionalidades:

- Verificação em tempo real da cotação de um ativo escolhido pelo usuário
- Definição de um limite superior e um limite inferior
- Aviso via e-mail para recomendação de compra e venda
- Listagem de ativos checados anteriormente

## Verificação em tempo real

É necessário executar o seguinte comando:

```console
$ StockQuote.Console.exe PETR4 22.67 22.59
```

- Argumento 1: ticker do ativo da B3 (PETR4, ITUB3, por exemplo)
- Argumento 2: limite superior para ser avisado caso o preço do ativo esteja subindo
- Argumento 3: limite inferior

## Listagem

Para realizar a listagem de ativos, é necessário executar o seguinte comando:

```console
$ StockQuote.Console.exe -l
```
