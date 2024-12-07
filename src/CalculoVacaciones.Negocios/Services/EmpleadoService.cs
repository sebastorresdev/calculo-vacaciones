using CalculoVacaciones.Data.Interfaces;
using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;

namespace CalculoVacaciones.Negocios.Services;
public class EmpleadoService : IEmpleadoService
{
    private readonly IEmpleadoRepository _empleadoRepository;

    public EmpleadoService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public List<Empleado> Listar()
    {
        return _empleadoRepository.Listar();
    }

    public Empleado ObtenerPorId(int id)
    {
        return _empleadoRepository.ObtenerPorId(id);
    }

    public int Registrar(Empleado empleado)
    {
        return _empleadoRepository.Registrar(empleado);
    }

    public bool Editar(Empleado empleado)
    {
        return _empleadoRepository.Editar(empleado);
    }

    public bool Elimiar(int id)
    {
        return _empleadoRepository.Eliminar(id);
    }
}
