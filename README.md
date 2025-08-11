# ClientesCRM – Backend (ASP.NET Core + EF Core + CQRS)

API REST para gestionar la entidad **Cliente**. El proyecto sigue una arquitectura por capas con **CQRS + MediatR**, **AutoMapper**, **EF Core** y el patrón **Repository**.

---

## Tabla de contenidos
- [Estructura del proyecto](#estructura-del-proyecto)
- [Arquitectura y patrones](#arquitectura-y-patrones)
- [Paradigma de programación](#paradigma-de-programación)
- [Endpoints](#endpoints)
- [Clases principales](#clases-principales)
- [Configuración y requisitos](#configuración-y-requisitos)
- [Puesta en marcha (desarrollo)](#puesta-en-marcha-desarrollo)
- [Decisiones de diseño](#decisiones-de-diseño)
- [Extensiones sugeridas](#extensiones-sugeridas)
- [Solución de problemas comunes](#solución-de-problemas-comunes)

---

## Estructura del proyecto

ClientesCRM/
└─ src/
├─ Core/ # Dominio puro
│ ├─ Entities/
│ │ └─ Cliente.cs
│ └─ Interfaces/
│ └─ IRepositories/
│ └─ IClienteRepository.cs
├─ Application/ # Casos de uso / CQRS / DTOs / Mapping
│ ├─ DTOs/
│ │ └─ ClienteDTOs/
│ │ ├─ ClienteCreateDto.cs
│ │ ├─ ClienteUpdateDto.cs
│ │ └─ ClienteResposeDto.cs
│ ├─ Commands/
│ │ ├─ ClienteCreateCommand.cs
│ │ ├─ ClienteUpdateCommand.cs
│ │ └─ ClienteDeleteCommand.cs
│ ├─ Queries/
│ │ └─ GetClienteByIdQuery.cs
│ ├─ Handlers/
│ │ ├─ ClienteCreateHandler.cs
│ │ ├─ ClienteUpdateHandler.cs
│ │ ├─ ClienteDeleteHandler.cs
│ │ └─ GetClienteByIdHandler.cs
│ └─ AutoMapper/
│ └─ MapperProfile.cs
├─ Infrastructure/ # Acceso a datos / EF Core
│ ├─ Data/
│ │ ├─ ClientesDbContext.cs
│ │ └─ ConfigContext/
│ │ └─ ClienteConfigContext.cs
│ └─ Persistence/
│ └─ Repositories/
│ └─ ClienteRepository.cs
└─ API/ # Capa de presentación HTTP
├─ Controllers/
│ └─ ClienteController.cs
├─ Program.cs
└─ appsettings.json


---

## Arquitectura y patrones

- **CQRS** (Command Query Responsibility Segregation): separación de lectura/escritura.
- **Mediator (MediatR):** handlers procesan comandos/queries desacoplados del controller.
- **Repository Pattern:** `IClienteRepository` encapsula la persistencia (EF Core detrás).
- **DTOs + AutoMapper:** contrato de entrada/salida desacoplado del modelo de dominio.
- **Fluent Configuration (EF Core):** configuración por entidad en `ClienteConfigContext`.
- **Inversión de dependencias:** servicios y repositorios registrados en `Program.cs`.
- **Capas limpias:** `Core` no depende de `Infrastructure` ni de `API`.

---

## Paradigma de programación

- **Orientado a objetos**, principios **SOLID**, asincronía con `async/await`.
- Enfoque **DDD-lite** (entidades del dominio + orquestación en la capa de aplicación).

---

## Endpoints

Base: `/api/clientes`

- `GET  /api/clientes`             → Lista de clientes (`IReadOnlyList<ClienteResposeDto>`).
- `GET  /api/clientes/{id}`        → Obtener un cliente por Id.
- `POST /api/clientes`             → Crear un cliente (devuelve `ClienteResposeDto` con `Id`).
- `PUT  /api/clientes/{id}`        → Actualizar **contacto** (teléfono, correo, dirección principal).
- `DELETE /api/clientes/{id}`      → Eliminar un cliente.

---

## Clases principales

### Dominio (`Core/Entities`)
- **`Cliente`**  
  Entidad del dominio con: `Id` (Guid), `Nombre`, `Apellido`, `Telefono`, `CorreoElectronico`, `DireccionPrincipal`, `NotasGenerales`.

### Aplicación – DTOs (`Application/DTOs/ClienteDTOs`)
- **`ClienteCreateDto`**: datos requeridos para **crear** (todos menos `Id`).
- **`ClienteUpdateDto`**: datos para **actualizar contacto** (teléfono, correo, dirección).
- **`ClienteResposeDto`**: datos de **salida** (incluye `Id` + datos del cliente).

### Aplicación – CQRS
- **Commands**  
  - `ClienteCreateCommand` → alta de cliente.  
  - `ClienteUpdateCommand` → actualización de contacto.  
  - `ClienteDeleteCommand` → baja por Id.
- **Queries**  
  - `GetClienteByIdQuery` → lectura por Id.  
  - (Opcional) `GetAllClientesQuery` → listado inmutable (`IReadOnlyList`).
- **Handlers**  
  - `ClienteCreateHandler`, `ClienteUpdateHandler`, `ClienteDeleteHandler`, `GetClienteByIdHandler`.  
    Orquestan: validación básica, repositorio, mapping entidad⇄DTO.

### Mapping (`Application/AutoMapper`)
- **`MapperProfile`**  
  `CreateMap<Cliente, ClienteResposeDto>` y mappings para `Create/Update`.

### Infraestructura – EF Core
- **`ClientesDbContext`**  
  `DbSet<Cliente>`; en `OnModelCreating` aplica configuraciones con `ApplyConfigurationsFromAssembly`.
- **`ClienteConfigContext`**  
  Mapea la entidad a la tabla `Clientes`, claves, longitudes, nullability, etc.
- **`ClienteRepository`**  
  Implementación de `IClienteRepository` (CRUD async con EF Core).

### API
- **`ClienteController`**  
  Endpoints REST que envían/reciben DTOs y delegan en MediatR.
- **`Program.cs`**  
  Bootstrap: DI, CORS, AutoMapper, MediatR, EF Core (`DefaultConnection`), pipeline.  
  En desarrollo puede usar `EnsureCreated()` o `MigrateAsync()` para levantar el esquema.

---

## Configuración y requisitos

- **.NET SDK 8** (o la versión usada en el proyecto).
- **SQL Server Express** (recomendado) o **LocalDB**.
- **CORS** permitido para el front (por ejemplo `http://localhost:5173`).

**Cadenas de conexión (appsettings.json)**
```json
// Express
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=ClientesCRM;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False"

CORS en Program.cs (ejemplo)

csharp
Copiar
Editar
const string FrontendDevCors = "FrontendDev";
builder.Services.AddCors(o => o.AddPolicy(FrontendDevCors, p =>
    p.WithOrigins("http://localhost:5173","http://127.0.0.1:5173")
     .AllowAnyHeader()
     .AllowAnyMethod()
));
...
app.UseCors(FrontendDevCors);

EF Core con resiliencia

csharp
Copiar
Editar
builder.Services.AddDbContext<ClientesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure() // reintentos ante fallos transitorios
    )
);

--

Puesta en marcha (desarrollo)
Ajusta la cadena de conexión en appsettings.json.

(Opcional) Crea la BD en SSMS y otorga permisos a tu usuario de Windows.

Asegura el bootstrap del esquema tras builder.Build():

csharp
Copiar
Editar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientesDbContext>();
    await db.Database.EnsureCreatedAsync(); // rápido en dev
    // o bien: await db.Database.MigrateAsync(); // formal con migraciones
}
Restaura y ejecuta:

bash
Copiar
Editar
dotnet restore
dotnet run
Probar en navegador:

GET http://localhost:<puerto>/api/clientes → [] (inicialmente).
