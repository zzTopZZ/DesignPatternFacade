# 🏗️ Design Pattern Facade - Sistema de Concessão de Empréstimos

Projeto demonstrativo do **padrão de projeto Facade (Fachada)** aplicado a uma Web API em **.NET 8**. A API simula um sistema de análise de crédito que orquestra múltiplos subsistemas através de uma fachada única e simplificada.

## 🧐 O que é o Padrão Facade?

O **Facade** é um padrão de projeto estrutural que fornece uma interface simplificada para um conjunto complexo de subsistemas. Em vez do cliente (Controller) precisar conhecer e interagir com cada subsistema individualmente, ele se comunica apenas com a fachada, que coordena todas as operações internamente.

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
?   ??? Facade.cs                  # Interface e implementa��o da Fachada
??? Subsistemas/
?   ??? Cadastro.cs                # Registra o cliente no sistema
?   ??? Cadin.cs                   # Consulta restri��es no CADIN
?   ??? Serasa.cs                  # Consulta pend�ncias no SERASA
?   ??? LimiteCredito.cs           # Verifica se o valor est� dentro do limite
??? Program.cs                     # Configura��o da aplica��o e DI
```

## ?? Como Funciona

### A Fachada (`MeuFacade`)

A classe `MeuFacade` implementa a interface `IMeuFacade` e encapsula toda a l�gica de an�lise de cr�dito. O controller s� precisa chamar um �nico m�todo:

```csharp
bool aprovado = _facade.ConcederEmprestimo(cliente, valor);
```

### Subsistemas Orquestrados

Internamente, a fachada coordena **4 subsistemas** em sequ�ncia:

| # | Subsistema       | Responsabilidade                                                    |
|---|------------------|---------------------------------------------------------------------|
| 1 | **Cadastro**     | Registra o cliente no banco de dados (simula��o)                    |
| 2 | **CADIN**        | Verifica se o cliente possui restri��es no Cadastro Informativo     |
| 3 | **SERASA**       | Verifica se o cliente possui pend�ncias financeiras                 |
| 4 | **LimiteCredito**| Verifica se o valor solicitado est� dentro do limite (R$ 100.000)   |

Se **qualquer** verifica��o falhar, o empr�stimo � negado imediatamente (fail-fast).

### Inje��o de Depend�ncia

Todos os subsistemas e a fachada s�o registrados no container de DI nativo do .NET:

```csharp
builder.Services.AddScoped<Cadastro>();
builder.Services.AddScoped<Cadin>();
builder.Services.AddScoped<Serasa>();
builder.Services.AddScoped<LimiteCredito>();
builder.Services.AddScoped<IMeuFacade, MeuFacade>();
```

---

## ?? Tecnologias Utilizadas

| Tecnologia               | Vers�o | Descri��o                                   |
|--------------------------|--------|---------------------------------------------|
| **.NET**                 | 8.0    | Framework principal da aplica��o             |
| **ASP.NET Core Web API** | �      | Framework para constru��o da API REST        |
| **Swashbuckle**          | 6.6.2  | Swagger UI para documenta��o e teste da API  |

---

## ?? Como Executar

### Pr�-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Passos

1. **Clone o reposit�rio:**
   ```bash
   git clone https://github.com/zzTopZZ/DesignPatternFacade.git
   cd DesignPatternFacade
   ```

2. **Execute a aplica��o:**
   ```bash
   dotnet run --project RunFacade
   ```

3. **Acesse o Swagger:**
   ```
   https://localhost:{porta}/swagger
   ```
   > A porta ser� exibida no terminal ao iniciar a aplica��o.

---

## ?? Como Usar a API

### Endpoint

```
POST /api/Emprestimo/analisar
```

### Request Body (JSON)

```json
{
  "nomeCliente": "Jo�o Silva",
  "valor": 50000
}
```

### Exemplo com cURL

```bash
curl -X POST https://localhost:{porta}/api/Emprestimo/analisar \
  -H "Content-Type: application/json" \
  -d '{"nomeCliente": "Jo�o Silva", "valor": 50000}'
```

### Respostas

**? Aprovado (200 OK)**

```json
{
  "status": "Aprovado",
  "mensagem": "Cr�dito concedido para Jo�o Silva"
}
```

**? Negado (400 Bad Request)**

```json
{
  "status": "Negado",
  "mensagem": "Restri��es encontradas ou limite insuficiente"
}
```

---

## ?? Testando

1. Abra o **Swagger UI** no navegador.
2. Expanda o endpoint `POST /api/Emprestimo/analisar`.
3. Clique em **Try it out**.
4. Envie um valor de at� `100000` para obter aprova��o.
5. Envie um valor acima de `100000` para simular nega��o por limite de cr�dito.

---

## ?? Refer�ncias

- [Facade Pattern � Refactoring Guru](https://refactoring.guru/design-patterns/facade)

---

## ?? Licen�a

Este projeto � disponibilizado sob a licen�a **MIT**.
