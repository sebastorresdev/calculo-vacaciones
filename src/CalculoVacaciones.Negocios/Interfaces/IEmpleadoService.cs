using CalculoVacaciones.Data.Models;

namespace CalculoVacaciones.Negocios.Interfaces;
public interface IEmpleadoService
{
    List<Empleado> Listar();
    Empleado ObtenerPorId(int idEmpleado);
    bool Editar(Empleado empleado);
    int Guardar(Empleado empleado);
    bool Eliminar(int idEmpleado);
}
