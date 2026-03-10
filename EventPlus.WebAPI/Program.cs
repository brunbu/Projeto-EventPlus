using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventContext>(opitions =>
    opitions.UseSqlServer
    (builder.Configuration.GetConnectionString
    ("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API para gerenciamento de eventos",
        Description = "Aplicação para gerenciamneto de eventos",
        TermsOfService = new Uri("https://https://github.com/brunbu/terms"),
        Contact = new OpenApiContact
        {
            Name = "Equipe de Suporte",
            Url = new Uri("https://https://github.com/brunbu/support")

        },
        License = new OpenApiLicense
        {
            Name = "Exemplo de Licença",
            Url = new Uri("https://example.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"

    });
    options.AddSecurityRequirement(document => new
    OpenApiSecurityRequirement
    {

        [new OpenApiSecuritySchemeReference("Berear", document)] = Array.Empty<string>().ToList()

    });
    
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
