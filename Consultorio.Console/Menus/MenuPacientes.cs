using Consultorio.Application.Pacientes;

namespace Consola.Menus;

internal sealed class MenuPacientes(PacienteServicio servicio) : MenuPlantilla
{
    protected override void MostrarOpciones()
    {
        Console.WriteLine("--- Pacientes ---");
        Console.WriteLine(" 1. Listar activos");
        Console.WriteLine(" 2. Alta");
        Console.WriteLine(" 3. Baja lógica");
        Console.WriteLine(" 0. Volver");
    }

    protected override async Task EjecutarOpcionAsync(string opcion)
    {
        switch (opcion)
        {
            case "1": await ListarAsync(); break;
            case "2": await AltaAsync(); break;
            case "3": await BajaAsync(); break;
            default: Console.WriteLine("Opción inválida."); break;
        }
    }

    async Task ListarAsync()
    {
        var resultado = await servicio.ListarActivosAsync();
        if (Salida.MostrarSiFallo(resultado))
            return;
        if (resultado.Valor.Count == 0)
        {
            Console.WriteLine("No hay pacientes activos.");
            return;
        }

        foreach (var p in resultado.Valor)
            Console.WriteLine($"[{p.Dni}] {p.Nombre}");
    }

    async Task AltaAsync()
    {
        var dni = Lector.LeerTexto("DNI");
        var nombre = Lector.LeerTexto("Nombre");
        var resultado = await servicio.CrearAsync(dni, nombre);
        if (Salida.MostrarSiFallo(resultado))
            return;
        Console.WriteLine($"Paciente {resultado.Valor.Dni} creado.");
    }

    async Task BajaAsync()
    {
        var dni = Lector.LeerTexto("DNI");
        var resultado = await servicio.DarDeBajaAsync(dni);
        if (Salida.MostrarSiFallo(resultado))
            return;
        Console.WriteLine($"Paciente {dni} dado de baja.");
    }
}
