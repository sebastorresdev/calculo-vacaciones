using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CalculoVacaciones.FrontEnd.Controllers
{
    public class CalculoVacacionesController : Controller
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly ITipoEmpleadoService _tipoEmpleadoService;
        private readonly IVacacionService _vacacionService;

        public CalculoVacacionesController(IEmpleadoService empleadoService, ITipoEmpleadoService tipoEmpleadoService, IVacacionService vacacionService)
        {
            _empleadoService = empleadoService;
            _tipoEmpleadoService = tipoEmpleadoService;
            _vacacionService = vacacionService;
        }

        public IActionResult Index()
        {
            // Obtener lista de empleados y enviarla a la vista
            var empleados = _empleadoService.Listar();
            ViewBag.Empleados = empleados;

            // Crear modelo vacío para inicializar la vista
            Vacaciones vacaciones = new Vacaciones();
            return View(vacaciones);
        }

        [HttpPost]
        public IActionResult CalcularVacaciones(Vacaciones vacaciones)
        {
            if (ModelState.IsValid)
            {
                // Realizar el cálculo
                var resultado = CalcularVacacionesDisponibles(vacaciones.FechaSeleccionada, vacaciones.Id);

                // Enviar resultado a la vista
                ViewBag.Empleados = _empleadoService.Listar();
                return View("Index", resultado);
            }

            // Si el modelo no es válido, recargar empleados y regresar a la vista
            ViewBag.Empleados = _empleadoService.Listar();
            return View("Index", vacaciones);
        }

        private Vacaciones CalcularVacacionesDisponibles(DateTime fechaSeleccionada, int id)
        {
            // Obtener datos del empleado
            var empleado = _empleadoService.ObtenerPorId(id);

            // Obtener tipo de empleado para calcular las vacaciones
            var tipoEmpleado = _tipoEmpleadoService.ObtenerPorId(empleado.IdTipoEmpleado);

            // Calcular meses trabajados
            var mesesTrabajados = (fechaSeleccionada.Year - empleado.FechaIngreso.Year) * 12
                                  + (fechaSeleccionada.Month - empleado.FechaIngreso.Month);

            var diasAcumulados = (mesesTrabajados / 12.0) * tipoEmpleado.DiasVacacionesAnuales;

            // 2. Obtener días ya tomados (aprobados)
            var diasTomados = _vacacionService.ObtenerDiasTomados(id, fechaSeleccionada);

            // 3. Calcular días disponibles
            var diasDisponibles = diasAcumulados - diasTomados;

            return new Vacaciones()
            {
                Empleado = empleado,
                VacacionesDisponibles = (int)diasDisponibles,
                FechaSeleccionada = fechaSeleccionada
            };
        }
    }
}
