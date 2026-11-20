# Lab10-AntonyHuayhua

Alumno: Lukas Antonio Melgar Casimiro.

API de tickets en .NET 10 y PostgreSQL. Capas: Domain (entidades y contratos),
Application (DTOs y casos de uso), Infrastructure (EF Core, repositorios y JWT)
y Api (controladores y configuraci?n centralizada).
Se conserva Unit of Work y BCrypt, que ya utilizaba el proyecto.

## Ejecutar

1. La base local `support_tickets_db` ya existe y se verifico con el usuario `postgres`. No volver a importar el SQL sobre sus tablas.
2. Ajustar `ConnectionStrings:DefaultConnection` en
   `Lab10-AntonyHuayhua.Api/appsettings.json` con la base y credenciales locales.
3. Desde esta carpeta:

```powershell
dotnet build
dotnet run --project .\Lab10-AntonyHuayhua.Api --launch-profile http
```

Swagger: http://localhost:5236/swagger
La clave JWT incluida es para la pr?ctica local y se puede sustituir con `Jwt__Secret`.
No se crean ni modifican bases de datos autom?ticamente.

## Alcance de la limpieza

La gu?a local de Lab 10 pide arquitectura por capas, repositorios, Swagger y JWT;
no enumera endpoints espec?ficos. Se conserva el CRUD b?sico de sus cinco tablas,
registro/login y actualizaci?n del estado del ticket. Se retiran el registro duplicado
en Users, cambio de contrase?a, cambio de rol, cierre duplicado del ticket y consultas
especializadas por usuario/rol, con sus casos de uso y m?todos sin consumidores.
Persistense estaba vac?a; el DbContext se mantiene ?nicamente en Infrastructure.
Se eliminan paquetes de dise?o, herramientas y OpenAPI redundantes de la API.
No se encontr? una gu?a o proyecto Lab 09 para una comparaci?n exacta.

Las carpetas bin/obj se eliminan despu?s de verificar; dotnet build las vuelve a generar.
Ver docs/GUIA_PRUEBAS.md y el archivo .http para las pruebas manuales.

## Verificacion con la guia y PostgreSQL

Se revisaron tambien las capturas con codigo del documento.
Consultar docs/CUMPLIMIENTO_LAB10.md, docs/EVIDENCIA_API.json y docs/swagger.json.
El flujo de registro, JWT, tickets y respuestas se verifico contra la base real.
