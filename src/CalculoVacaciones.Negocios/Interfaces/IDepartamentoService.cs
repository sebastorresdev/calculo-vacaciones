using CalculoVacaciones.Data.Models;

namespace CalculoVacaciones.Negocios.Interfaces;
public interface IDepartamentoService
{
    List<Departamento> Listar();
    Departamento ObtenerPorId(int idDepartamento);
    bool Editar(Departamento departamento);
    bool Eliminar(int idDepartamento);
    bool Guardar(Departamento departamento);
}
