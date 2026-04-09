# ??? Design Pattern Facade - Sistema de Concessão de Empréstimos

Projeto demonstrativo do **padrão de projeto Facade (Fachada)** aplicado a uma Web API em **.NET 8**. A API simula um sistema de análise de crédito que orquestra múltiplos subsistemas através de uma fachada única e simplificada.

## ?? O que é o Padrão Facade?

O **Facade** é um padrão de projeto estrutural que fornece uma interface simplificada para um conjunto complexo de subsistemas. Em vez do cliente (controller) precisar conhecer e interagir com cada subsistema individualmente, ele se comunica apenas com a fachada, que coordena todas as operações internamente.

```
                         ????????????????
     HTTP Request ??????>?  Controller  ?
                         ????????????????
                                ?
                         ????????????????
                         ?   Facade     ?  ? Interface simplificada
                         ????????????????
                  ???????????????????????????????
            ?????????? ????????? ????????? ???????????????
            ?Cadastro? ? Cadin ? ? Serasa? ?LimiteCredito?
            ?????????? ????????? ????????? ???????????????
                         Subsistemas
```

## ??? Estrutura do Projeto

```
RunFacade/
??? Controllers/
?   ??? EmprestimoController.cs   # Endpoint da API
??? Entities/
?   ??? Cliente.cs                 # Record que representa o cliente
??? Facades/
?   ??? Facade.cs                  # Interface e implementação da Fachada
??? Subsistemas/
?   ??? Cadastro.cs                # Registra o cliente no sistema
?   ??? Cadin.cs                   # Consulta restrições no CADIN
?   ??? Serasa.cs                  # Consulta pendências no SERASA
?   ??? LimiteCredito.cs           # Verifica se o valor está dentro do limite
??? Program.cs                     # Configuração da aplicação e DI
```

## ?? Como Funciona

### A Fachada (`MeuFacade`)

A classe `MeuFacade` implementa a interface `IMeuFacade` e encapsula toda a lógica de análise de crédito. O controller só precisa chamar um único método:

```csharp
bool aprovado = _facade.ConcederEmprestimo(cliente, valor);
```

### Subsistemas Orquestrados

Internamente, a fachada coordena **4 subsistemas** em sequência:

| # | Subsistema       | Responsabilidade                                                    |
|---|------------------|---------------------------------------------------------------------|
| 1 | **Cadastro**     | Registra o cliente no banco de dados (simulação)                    |
| 2 | **CADIN**        | Verifica se o cliente possui restrições no Cadastro Informativo     |
| 3 | **SERASA**       | Verifica se o cliente possui pendências financeiras                 |
| 4 | **LimiteCredito**| Verifica se o valor solicitado está dentro do limite (R$ 100.000)   |

Se **qualquer** verificação falhar, o empréstimo é negado imediatamente (fail-fast).

### Injeção de Dependência

Todos os subsistemas e a fachada são registrados no container de DI nativo do .NET:

```csharp
builder.Services.AddScoped<Cadastro>();
builder.Services.AddScoped<Cadin>();
builder.Services.AddScoped<Serasa>();
builder.Services.AddScoped<LimiteCredito>();
builder.Services.AddScoped<IMeuFacade, MeuFacade>();
```

---

## ?? Tecnologias Utilizadas

| Tecnologia               | Versão | Descrição                                   |
|--------------------------|--------|---------------------------------------------|
| **.NET**                 | 8.0    | Framework principal da aplicação             |
| **ASP.NET Core Web API** | —      | Framework para construção da API REST        |
| **Swashbuckle**          | 6.6.2  | Swagger UI para documentação e teste da API  |

---

## ?? Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Passos

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/zzTopZZ/DesignPatternFacade.git
   cd DesignPatternFacade
   ```

2. **Execute a aplicação:**
   ```bash
   dotnet run --project RunFacade
   ```

3. **Acesse o Swagger:**
   ```
   https://localhost:{porta}/swagger
   ```
   > A porta será exibida no terminal ao iniciar a aplicação.

---

## ?? Como Usar a API

### Endpoint

```
POST /api/Emprestimo/analisar
```

### Request Body (JSON)

```json
{
  "nomeCliente": "João Silva",
  "valor": 50000
}
```

### Exemplo com cURL

```bash
curl -X POST https://localhost:{porta}/api/Emprestimo/analisar \
  -H "Content-Type: application/json" \
  -d '{"nomeCliente": "João Silva", "valor": 50000}'
```

### Respostas

**? Aprovado (200 OK)**

```json
{
  "status": "Aprovado",
  "mensagem": "Crédito concedido para João Silva"
}
```

**? Negado (400 Bad Request)**

```json
{
  "status": "Negado",
  "mensagem": "Restrições encontradas ou limite insuficiente"
}
```

---

## ?? Testando

1. Abra o **Swagger UI** no navegador.
2. Expanda o endpoint `POST /api/Emprestimo/analisar`.
3. Clique em **Try it out**.
4. Envie um valor de até `100000` para obter aprovação.
5. Envie um valor acima de `100000` para simular negação por limite de crédito.

---

## ?? Referências

- [Facade Pattern — Refactoring Guru](https://refactoring.guru/design-patterns/facade)

---

## ?? Licença

Este projeto é disponibilizado sob a licença **MIT**.
