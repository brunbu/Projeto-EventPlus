using Azure.AI.ContentSafety;
using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var endpoint = "";
var apiKey = "";
builder.Services.AddSingleton(new ContentSafetyClient(
    new Uri(endpoint),
    new Azure.AzureKeyCredential(apiKey)
));

var client = new ContentSafetyClient(new Uri(endpoint), new Azure.AzureKeyCredential(apiKey));

builder.Services.AddDbContext<EventContext>(opitionsAction =>
{
opitionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IPresencaRepository, PresencaRepository>();
builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();


//adiciona o ser viço de jwt Bearer (metodo de autenticação)

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem está solucionando
        ValidateIssuer = true,
        //valida quem está solucionando
        ValidateAudience = true,
        //valida se o tempo de expiração será validado
        ValidateLifetime = true,
        //forma de criptografia e valida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("EventPlus-chave-autenticacao-webapi-dev")),

        //valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //nome do issuer (de onde está vindo)
        ValidIssuer = "api_event",

        //nome do audience (para onde ele está indo)
        ValidAudience = "api_event"

    };
});

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
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"

    });
    options.AddSecurityRequirement(document => new
    OpenApiSecurityRequirement
    {

        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()

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
