using CalculoVacaciones.Data.Models;

namespace CalculoVacaciones.Negocios.Interfaces;
public interface ITipoEmpleadoService
{
    List<TipoEmpleado> Listar();
    TipoEmpleado ObtenerPorId(int idTipoEmpleado);
    bool Editar(TipoEmpleado tipoEmpleado);
    bool Eliminar(int idTipoEmpleado);
    bool Guardar(TipoEmpleado tipoEmpleado);
}
