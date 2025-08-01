using System.Reflection;
using ClientesCRM.src.Application.AutoMapper;
using ClientesCRM.src.Application.Handlers.ClienteHandlers;
using ClientesCRM.src.Core.Interfaces.IQueries.IClienteQueries;
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
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientesDbContext>();
    // Creaamos la base de datos
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi(); // TODO: usar swagger para la documentacion de la API
// }

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
