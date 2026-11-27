using Consultorio.Application.Turnos;

namespace Consola.Menus;

internal sealed class MenuTurnos(TurnoServicio servicio) : MenuPlantilla
{
    protected override void MostrarOpciones()
    {
        Console.WriteLine("--- Turnos ---");
        Console.WriteLine(" 1. Nuevo turno");
        Console.WriteLine(" 2. Cancelar turno");
        Console.WriteLine(" 3. Agenda del día");
        Console.WriteLine(" 0. Volver");
    }

    protected override async Task EjecutarOpcionAsync(string opcion)
    {
        switch (opcion)
        {
            case "1": await NuevoTurnoAsync(); break;
            case "2": await CancelarAsync(); break;
            case "3": await AgendaDelDíaAsync(); break;
            default: Console.WriteLine("Opción inválida."); break;
        }
    }

    async Task NuevoTurnoAsync()
    {
        var matrícula = Lector.LeerTexto("Matrícula del profesional");
        var dni = Lector.LeerTexto("DNI del paciente");
        var inicio = Lector.LeerInicio();

        var resultado = await servicio.ReservarAsync(matrícula, dni, inicio);
        if (Salida.MostrarSiFallo(resultado))
            return;

        var t = resultado.Valor;
        Console.WriteLine($"Turno #{t.Id} confirmado: {t.Inicio:dd/MM/yyyy HH:mm} - {t.Fin:HH:mm} | Importe: {t.Importe:C2}");
    }

    async Task CancelarAsync()
    {
        var id = Lector.LeerEntero("Id del turno");
        var resultado = await servicio.CancelarAsync(id);
        if (Salida.MostrarSiFallo(resultado))
            return;

        Console.WriteLine($"Turno #{id} cancelado.");
    }

    async Task AgendaDelDíaAsync()
    {
        var fecha = Lector.LeerFecha("Fecha");
        var resultado = await servicio.ListarAgendaDelDíaAsync(fecha);
        if (Salida.MostrarSiFallo(resultado))
            return;

        if (resultado.Valor.Count == 0)
        {
            Console.WriteLine("No hay turnos para esa fecha.");
            return;
        }

        foreach (var t in resultado.Valor)
        {
            Console.WriteLine(
                $"#{t.Id} | {t.NombreProfesional} [{t.MatrículaProfesional}] | {t.Inicio:HH:mm}-{t.Fin:HH:mm} | {t.NombrePaciente} ({t.DniPaciente}) | {t.Estado} | {t.Importe:C2}");
        }
    }
}
