using Api.Endpoints;
using System.Text.Json;
using Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(builder.Configuration);

var app = builder.Build();

//Captura global de exeptions, dessa forma nao precisa
//utilizar try cath nos metodos
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

#if DEBUG
        var error = new
        {
            Message = "Ocorreu um erro interno.",
            Detail = $"Exception: {ex.Message}. {Environment.NewLine} Stacktrace: {ex.StackTrace}"  // ou ex.ToString() para stacktrace
        };
        var json = JsonSerializer.Serialize(error);
        await context.Response.WriteAsync(json);
#else
        var error = new
        {
            Message = "Ocorreu um erro interno.",
            Detail = Aguarde uns instantes para nova tentativa
        };
        var json = JsonSerializer.Serialize(error);
        await context.Response.WriteAsync(json);
#endif
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Carrega todos os Endpois que estao com a interface IMap dentro de maps
var maps = typeof(Program).Assembly
    .GetTypes()
    .Where(t => typeof(Iendpoints).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
    .Select(Activator.CreateInstance)
    .Cast<Iendpoints>();

foreach (var map in maps)
    map.Map(app);

app.Run();
app.Run();