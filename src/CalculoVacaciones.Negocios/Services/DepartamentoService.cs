using CalculoVacaciones.Data.Interfaces;
using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;

namespace CalculoVacaciones.Negocios.Services;
public class DepartamentoService : IDepartamentoService
{
    private readonly IDepartamentoRepository _departamentoRepository;

    public DepartamentoService(IDepartamentoRepository departamentoRepository)
    {
        _departamentoRepository = departamentoRepository;
    }

    public List<Departamento> Listar()
    {
        return _departamentoRepository.Listar();
    }
}
