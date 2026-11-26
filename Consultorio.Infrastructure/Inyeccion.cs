using Consultorio.Application.Abstracciones;
using Consultorio.Domain.Repositorios;
using Consultorio.Infrastructure.Persistencia;
using Consultorio.Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Consultorio.Infrastructure;

public static class Inyeccion
{
    public static IServiceCollection AgregarInfraestructura(this IServiceCollection services, string cadena)
    {
        services.AddDbContext<ConsultorioDbContext>(opciones => opciones.UseSqlServer(cadena));
        services.AddScoped<IProfesionalRepositorio, ProfesionalRepositorio>();
        services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();
        services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

        // TODO (Punto 4): Registrar ITurnoRepositorio con TurnoRepositorio usando services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();

        return services;
    }
}
