using System.Reflection;
using ClientesCRM.src.Application.AutoMapper;
using ClientesCRM.src.Application.Handlers.ClienteHandlers;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using ClientesCRM.src.Infrastructure.Data;
using ClientesCRM.src.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); // cambiar por swagger para la documentacion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

const string FrontendDevCors = "FrontendDev";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: FrontendDevCors, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
            // .AllowCredentials(); // <-- solo para cookies/autenticación basada en navegador
    });
});

// AutoMapper
builder.Services.AddAutoMapper(
    cfg =>
    {
        cfg.AddProfile<MapperProfile>();
    }
);

// Mediador, usaremos MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<ClienteCreateHandler>());

// QueryHandler TODO: cambiar esto por la implementacion del MediatR
// builder.Services.AddScoped<IGetClienteByIdHandler, GetClienteByIdHandler>();

// Rpository
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();


// Context
builder.Services.AddDbContext<ClientesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure()
    )
);

var app = builder.Build();
// Crea la tabla de clientes
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientesDbContext>();
    await db.Database.EnsureCreatedAsync();
}


// Bloque de codigo que se usara en la primera ejecucion para crear la base de datos 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientesDbContext>();

    if (!await db.Database.CanConnectAsync())
    {
        await db.Database.EnsureCreatedAsync();
    }
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi(); // TODO: usar swagger para la documentacion de la API
// }

app.UseCors(FrontendDevCors);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
