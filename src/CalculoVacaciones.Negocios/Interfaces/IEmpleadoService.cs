using CalculoVacaciones.Data.Models;

namespace CalculoVacaciones.Negocios.Interfaces;
public interface IEmpleadoService
{
    List<Empleado> Listar();
    Empleado ObtenerPorId(int id);
    bool Editar(Empleado empleado);
    int Registrar(Empleado empleado);
    bool Elimiar(int id);
}
