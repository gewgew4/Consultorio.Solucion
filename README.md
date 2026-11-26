# Consultorio (Plantilla de Recuperatorio)

Solución base en C# 13 / .NET 9 con arquitectura en capas y persistencia en SQL Server mediante EF Core 9 (sin migraciones).

## Qué incluye el repositorio

- `Consultorio.Domain`: `Result`, `Result<T>`, `Error`, entidades `Profesional` y `Paciente`, interfaces de repositorio y `Errores`.
- `Consultorio.Application`: `IUnidadDeTrabajo`, `ProfesionalServicio`, `PacienteServicio` y sus DTOs.
- `Consultorio.Infrastructure`: `ConsultorioDbContext`, configuraciones Fluent API, repositorios EF Core e `Inyeccion`.
- `Consultorio.Console`: menú de consola, `Program.cs`, `Lector`, `Salida`, `MenuProfesionales`, `MenuPacientes` y `appsettings.json`.
- `nuget.config` y `nuget-offline/`: paquetes NuGet necesarios para restaurar y compilar sin conexión a internet.
- Comentarios `// TODO` en los archivos donde tenés que completar o conectar tu implementación.

## Base de datos (SQL Server, sin migraciones)

1. Este proyecto no utiliza migraciones de EF Core. Creá la base `Consultorio` y las tablas desde SQL Server Management Studio (SSMS) usando sentencias `CREATE TABLE` e `INSERT` según el enunciado de tu Tema.
2. Si tu instancia local no es `(localdb)\MSSQLLocalDB`, actualizá la cadena de conexión `Consultorio` en `Consultorio.Console/appsettings.json`.

## Qué tenés que entregar

1. Un único archivo `.zip` con la solución completa con tu nombre.
2. Eliminá antes de comprimir las carpetas `bin/`, `obj/`, `Debug/`, `Release/` y `Logs/`.
3. Incluí el archivo `Consultorio.sql` (con los `CREATE TABLE` y la carga inicial de datos) en la raíz de la entrega.
4. Verificá antes de entregar que la solución compile sin errores con `dotnet build` y respete la separación de capas (`Domain` y `Application` sin referencias a EF Core ni a `Console`; los menús solo llaman a servicios de `Application`).
