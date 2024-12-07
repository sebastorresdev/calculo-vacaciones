using CalculoVacaciones.Data.Interfaces;
using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoVacaciones.Negocios.Services;
public class TipoEmpleadoService : ITipoEmpleadoService
{
    private readonly ITipoEmpleadoRepository _tipoEmpleadoRepository;

    public TipoEmpleadoService(ITipoEmpleadoRepository tipoEmpleadoRepository)
    {
        _tipoEmpleadoRepository = tipoEmpleadoRepository;
    }

    public List<TipoEmpleado> Listar()
    {
        return _tipoEmpleadoRepository.Listar();
    }
}
