using APIRESTCRUDDAPPER.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "API REST CRUD DAPPER",
        Version = "v1",
        Description = "Api REST CRUD Utilizando Dapper + SQLServer",
        Contact = new OpenApiContact
        {
            Name = "Felipe Cabral",
            Url = new Uri("https://github.com/fgcabral88")
        },
        License = new OpenApiLicense
        {
            Name = "Licença MIT",
            Url = new Uri("https://opensource.com/licenses/MIT")
        },
        TermsOfService = new Uri("https://opensource.com/terms-of-service")
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "APIRESTCRUDDAPPERAnnotation.xml"));
});

builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

builder.Services.AddAutoMapper(typeof(Program)); 

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
