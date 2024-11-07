using APIRESTCRUDDAPPER.Application.Profiles.Profiles;
using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura o Serilog para usar a configuração do appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) 
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
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; // Assume versão padrão se não especificada
    options.DefaultApiVersion = new ApiVersion(1, 0); // Versão padrão
    options.ApiVersionReader = new QueryStringApiVersionReader("api-versao"); // Define como a versão será lida
    options.ReportApiVersions = true; // Relatar as versões disponíveis nas respostas
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
    app.UseSerilogRequestLogging();
    app.UseApiVersioning();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
