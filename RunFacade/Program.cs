using RunFacade.Facades;
using RunFacade.Subsistemas;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// 1. Registrar os Subsistemas
builder.Services.AddScoped<Cadastro>();
builder.Services.AddScoped<Cadin>();
builder.Services.AddScoped<Serasa>();
builder.Services.AddScoped<LimiteCredito>();

// 2. Registrar a Fachada (Facade)
builder.Services.AddScoped<IMeuFacade, MeuFacade>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Para testar via interface visual


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
