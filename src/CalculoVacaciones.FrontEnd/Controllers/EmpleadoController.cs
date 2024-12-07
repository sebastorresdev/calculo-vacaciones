using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalculoVacaciones.FrontEnd.Controllers;
public class EmpleadoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IEmpleadoService _empleadoService;
    private readonly IDepartamentoService _departamentoService;
    private readonly ITipoEmpleadoService _tipoEmpleadoService;

    public EmpleadoController(ILogger<HomeController> logger, 
        IEmpleadoService empleadoService, 
        IDepartamentoService departamentoService,
        ITipoEmpleadoService tipoEmpleadoService)
    {
        _logger = logger;
        _empleadoService = empleadoService;
        _departamentoService = departamentoService;
        _tipoEmpleadoService = tipoEmpleadoService;
    }

    public IActionResult Listar()
    {
        var empleados = _empleadoService.Listar();

        return View(empleados);
    }

    //Sirve para mostrar el formulario cuando se va registrar 
    [HttpGet]
    public IActionResult Guardar()
    {
        var departamentos = _departamentoService.Listar();
        var tipoEmpleados = _tipoEmpleadoService.Listar();

        ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");
        ViewBag.TipoEmpleados = new SelectList(tipoEmpleados, "Id", "Nombre");
        
        return View();
    }

    [HttpPost]
    //Obtiene los datos del formulario y lo envia a la base de datos 
    public IActionResult Guardar(Empleado empleado)
    {
        if (ModelState.IsValid)
        {
            // Guardar el empleado en la base de datos
            _empleadoService.Registrar(empleado);

            return RedirectToAction("Listar");  // O la acción correspondiente
        }

        // Si el modelo no es válido, se vuelve a mostrar la vista con los mensajes de error
        return View();
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var departamentos = _departamentoService.Listar();
        var tipoEmpleados = _tipoEmpleadoService.Listar();

        ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");
        ViewBag.TipoEmpleados = new SelectList(tipoEmpleados, "Id", "Nombre");

        var empleado = _empleadoService.ObtenerPorId(id);
        return View(empleado);
    }

    //Obtiene los datos del formulario y lo envia a la base de datos 
    [HttpPost]
    public IActionResult Editar(Empleado empleado)
    {
        if (ModelState.IsValid)
        {
            if (_empleadoService.Editar(empleado))
            {
                return RedirectToAction("Listar");
            }
        }
     
        ModelState.AddModelError("", "Error al actualizar");
        return View();
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        var empleado = _empleadoService.ObtenerPorId(id);
        return View(empleado);
    }

    [HttpPost]
    public IActionResult Eliminar(Empleado empleado)
    {
        if (_empleadoService.Elimiar(empleado.IdEmpleado))
        {
            return RedirectToAction("Listar");
        }
        else
        {
            ModelState.AddModelError("", "Error al actualizar");
            return View();
        }
    }
}
