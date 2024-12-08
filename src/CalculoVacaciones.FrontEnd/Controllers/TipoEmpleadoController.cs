using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using CalculoVacaciones.Negocios.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculoVacaciones.FrontEnd.Controllers;
public class TipoEmpleadoController : Controller
{
    private readonly ITipoEmpleadoService _tipoEmpleadoService;

    public TipoEmpleadoController(ITipoEmpleadoService tipoEmpleadoService)
    {
        _tipoEmpleadoService = tipoEmpleadoService;
    }

    public IActionResult Listar()
    {
        var tipoEmpleados = _tipoEmpleadoService.Listar();
        return View(tipoEmpleados);
    }

    [HttpGet]
    public IActionResult Guardar()
    {
        var tipoEmpleado = new TipoEmpleado();
        return View(tipoEmpleado);
    }

    [HttpPost]
    public IActionResult Guardar(TipoEmpleado tipoEmpleado)
    {
        if (ModelState.IsValid)
        {
            // Guardar el empleado en la base de datos
            _tipoEmpleadoService.Guardar(tipoEmpleado);

            return RedirectToAction("Listar");  // O la acción correspondiente
        }

        // Si el modelo no es válido, se vuelve a mostrar la vista con los mensajes de error
        return View();
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var tipoEmpleado = _tipoEmpleadoService.ObtenerPorId(id);
        return View(tipoEmpleado);
    }

    //Obtiene los datos del formulario y lo envia a la base de datos 
    [HttpPost]
    public IActionResult Editar(TipoEmpleado tipoEmpleado)
    {
        if (ModelState.IsValid)
        {
            if (_tipoEmpleadoService.Editar(tipoEmpleado))
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
        var tipoEmpleado = _tipoEmpleadoService.ObtenerPorId(id);
        return View(tipoEmpleado);
    }

    [HttpPost]
    public IActionResult Eliminar(TipoEmpleado tipoEmpleado)
    {
        if (_tipoEmpleadoService.Eliminar(tipoEmpleado.Id))
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
