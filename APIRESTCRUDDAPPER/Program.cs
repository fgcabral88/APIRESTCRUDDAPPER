using APIRESTCRUDDAPPER.Application.Profiles.Profiles;
using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Services;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura o Serilog para usar a configuração do appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Saída de logs no console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Saída de logs em arquivos
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "API REST",
        Version = "v1",
        Description = "CRUD Utilizando Dapper + AutoMapper + SQLServer + Log Serilog",
        Contact = new OpenApiContact { Name = "Felipe Cabral", Url = new Uri("https://github.com/fgcabral88") },
        License = new OpenApiLicense { Name = "Licença MIT", Url = new Uri("https://opensource.com/licenses/MIT") },
        TermsOfService = new Uri("https://opensource.com/terms-of-service")
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "APIRESTCRUDDAPPERAnnotation.xml"));
});

builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(ProfileAutoMapper));
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
