using Consultorio.Application.Profesionales;

namespace Consola.Menus;

internal sealed class MenuProfesionales(ProfesionalServicio servicio) : MenuPlantilla
{
    protected override void MostrarOpciones()
    {
        Console.WriteLine("--- Profesionales ---");
        Console.WriteLine(" 1. Listar");
        Console.WriteLine(" 2. Alta");
        Console.WriteLine(" 3. Deshabilitar");
        Console.WriteLine(" 0. Volver");
    }

    protected override async Task EjecutarOpcionAsync(string opcion)
    {
        switch (opcion)
        {
            case "1": await ListarAsync(); break;
            case "2": await AltaAsync(); break;
            case "3": await DeshabilitarAsync(); break;
            default: Console.WriteLine("Opción inválida."); break;
        }
    }

    async Task ListarAsync()
    {
        var resultado = await servicio.ListarAsync();
        if (Salida.MostrarSiFallo(resultado))
            return;
        if (resultado.Valor.Count == 0)
        {
            Console.WriteLine("No hay profesionales.");
            return;
        }

        foreach (var p in resultado.Valor)
        {
            var estado = p.Activo ? "activo" : "inactivo";
            Console.WriteLine($"[{p.Matrícula}] {p.Nombre}  {p.Especialidad}  {p.ValorConsulta:C0}  ({estado})");
        }
    }

    async Task AltaAsync()
    {
        var matrícula = Lector.LeerTexto("Matrícula");
        var nombre = Lector.LeerTexto("Nombre");
        var especialidad = Lector.LeerTexto("Especialidad");
        var valor = Lector.LeerDecimal("Valor consulta");
        var resultado = await servicio.CrearAsync(matrícula, nombre, especialidad, valor);
        if (Salida.MostrarSiFallo(resultado))
            return;
        Console.WriteLine($"Profesional {resultado.Valor.Matrícula} creado.");
    }

    async Task DeshabilitarAsync()
    {
        var matrícula = Lector.LeerTexto("Matrícula");
        var resultado = await servicio.DeshabilitarAsync(matrícula);
        if (Salida.MostrarSiFallo(resultado))
            return;
        Console.WriteLine($"Profesional {matrícula} deshabilitado.");
    }
}
