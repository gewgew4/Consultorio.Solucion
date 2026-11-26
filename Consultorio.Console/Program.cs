using Consola.Menus;
using Consultorio.Application.Pacientes;
using Consultorio.Application.Profesionales;
using Consultorio.Application.Turnos;
using Consultorio.Domain.Tarifas;
using Consultorio.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Globalization;
using System.Text;

namespace Consola;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var cultura = new CultureInfo("es-AR");
        CultureInfo.CurrentCulture = cultura;
        CultureInfo.CurrentUICulture = cultura;

        var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings { Args = args, ContentRootPath = AppContext.BaseDirectory });
        var logsDirectory = Path.Combine(FindSolutionRoot(AppContext.BaseDirectory), "Logs");
        Directory.CreateDirectory(logsDirectory);
        builder.Configuration["Serilog:WriteTo:0:Args:path"] = Path.Combine(logsDirectory, "log-.txt");

        builder.Logging.ClearProviders();
        builder.Services.AddSerilog((_, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

        var cadena = builder.Configuration.GetConnectionString("Consultorio") ?? "";
        builder.Services.AgregarInfraestructura(cadena);
        builder.Services.AddScoped<ProfesionalServicio>();
        builder.Services.AddScoped<PacienteServicio>();
        builder.Services.AddScoped<MenuProfesionales>();
        builder.Services.AddScoped<MenuPacientes>();
        builder.Services.AddScoped<ICalculadorTarifa, CalculadorTarifaVespertina>();
        builder.Services.AddScoped<TurnoServicio>();
        builder.Services.AddScoped<MenuTurnos>();

        using var host = builder.Build();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Consultorio iniciado.");

        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("===================================");
                Console.WriteLine("        CONSULTORIO v1.0");
                Console.WriteLine("===================================");
                Console.WriteLine(" 1. Profesionales");
                Console.WriteLine(" 2. Pacientes");
                Console.WriteLine(" 3. Turnos");
                Console.WriteLine(" 0. Salir");
                Console.WriteLine("-----------------------------------");
                var opcion = Lector.LeerTexto("Opción");
                if (opcion == "0")
                    return;

                using var alcance = host.Services.CreateScope();
                var proveedor = alcance.ServiceProvider;
                switch (opcion)
                {
                    case "1":
                        await proveedor.GetRequiredService<MenuProfesionales>().EjecutarAsync();
                        break;
                    case "2":
                        await proveedor.GetRequiredService<MenuPacientes>().EjecutarAsync();
                        break;
                    case "3":
                        await proveedor.GetRequiredService<MenuTurnos>().EjecutarAsync();
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error inesperado en la aplicación.");
                Console.WriteLine("Ocurrió un error inesperado. Revisá los logs e intentá nuevamente.");
            }
        }
    }

    static string FindSolutionRoot(string startPath)
    {
        for (var directory = new DirectoryInfo(startPath); directory is not null; directory = directory.Parent)
        {
            if (directory.EnumerateFiles("*.sln").Any())
                return directory.FullName;
        }

        return startPath;
    }
}
