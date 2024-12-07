using CalculoVacaciones.Data.Models;

namespace CalculoVacaciones.Data.Interfaces;
public interface IEmpleadoRepository
{
    List<Empleado> Listar();
    Empleado ObtenerPorId(int id);
    int Registrar(Empleado empleado);
    bool Editar(Empleado empleado);
    bool Eliminar(int idEmpleado);
}
