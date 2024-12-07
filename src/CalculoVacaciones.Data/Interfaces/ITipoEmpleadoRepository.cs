using CalculoVacaciones.Data.Models;

namespace CalculoVacaciones.Data.Interfaces;
public interface ITipoEmpleadoRepository
{
    List<TipoEmpleado> Listar();
}
