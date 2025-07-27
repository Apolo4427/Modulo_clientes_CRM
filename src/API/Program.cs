using ClientesCRM.src.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Context
builder.Services.AddDbContext<ClientesDbContext>( options => {
    options.UseSqlServer(cfg =>{
        builder.Configuration.GetConnectionString("DefaultConnection");
    })
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi(); // TODO: usar swagger para la documentacion de la API
// }

app.UseHttpsRedirection();



app.Run();
