# Consultorio (Tema 1)

Solución total en C# 13 / .NET 9 con arquitectura en capas y persistencia en SQL Server mediante EF Core 9 (sin migraciones).

## Base de datos (SQL Server, sin migraciones)

1. Este proyecto no utiliza migraciones de EF Core.

## Qué tenés que entregar

1. Un único archivo `.zip` con la solución completa con tu nombre.
2. Eliminá antes de comprimir las carpetas `bin/`, `obj/`, `Debug/`, `Release/` y `Logs/`.
3. Incluí el archivo `Consultorio.sql` (con los `CREATE TABLE` y la carga inicial de datos) en la raíz de la entrega.
4. Verificá antes de entregar que la solución compile sin errores con `dotnet build` y respete la separación de capas (`Domain` y `Application` sin referencias a EF Core ni a `Console`; los menús solo llaman a servicios de `Application`).
