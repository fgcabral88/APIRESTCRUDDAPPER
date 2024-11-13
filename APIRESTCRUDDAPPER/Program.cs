using APIRESTCRUDDAPPER.Application.Profiles.Profiles;
using APIRESTCRUDDAPPER.Application.Validations;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Infrastructure.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Data.SqlClient;
using System.Data;
using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Domain.Services.Services;
using APIRESTCRUDDAPPER.Infrastructure.Repositorys;
using APIRESTCRUDDAPPER.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    #region Swagger
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
    #endregion

    #region Identity Server
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT aqui",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    #endregion
});

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(ProfileAutoMapper));
#endregion

#region FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IValidator<UsuarioCriarDto>, UsuarioCriarDtoValidator>();
builder.Services.AddScoped<IValidator<UsuarioEditarDto>, UsuarioEditarDtoValidator>();
#endregion

#region Conexão com o banco de dados + Injecão de dependência
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); 
#endregion

#region Log
// Configura o Serilog para usar a configuração do appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();
#endregion

#region Health Checks - Verifica a saúde da aplicação
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "sqlserver");
#endregion

#region Identity Server
builder.Services.AddIdentityServer()
        .AddInMemoryClients(Config.ObtenhaClientes())
        .AddInMemoryApiScopes(Config.ObtenhaEscoposAPI())
        .AddInMemoryIdentityResources(Config.ObtenhaRecursosIdentidade())
        .AddInMemoryApiResources(Config.ObtenhaRecursosAPI())
        .AddDeveloperSigningCredential(); // Apenas para desenvolvimento
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseIdentityServer();
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message,
                duration = e.Value.Duration.TotalMilliseconds
            })
        });
        await context.Response.WriteAsync(result);
    }
});
app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
