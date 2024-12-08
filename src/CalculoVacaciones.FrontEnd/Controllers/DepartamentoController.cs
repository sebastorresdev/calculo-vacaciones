using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using CalculoVacaciones.Negocios.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculoVacaciones.FrontEnd.Controllers;
public class DepartamentoController : Controller
{
    private readonly IDepartamentoService _departamentoService;

    public DepartamentoController(IDepartamentoService departamentoService)
    {
        _departamentoService = departamentoService;
    }

    public IActionResult Listar()
    {
        var departamentos = _departamentoService.Listar();
        return View(departamentos);
    }

    public IActionResult Crear()
    {
        Departamento departamento = new();
        return View(departamento);
    }

    [HttpPost]
    public IActionResult Crear(Departamento departamento)
    {
        if (ModelState.IsValid)
        {
            // Guardar el empleado en la base de datos
            _departamentoService.Guardar(departamento);

            return RedirectToAction("Listar");  // O la acción correspondiente
        }

        // Si el modelo no es válido, se vuelve a mostrar la vista con los mensajes de error
        return View();
    }

    public IActionResult Editar(int id)
    {
        var departamento = _departamentoService.ObtenerPorId(id);
        return View(departamento);
    }

    [HttpPost]
    public IActionResult Editar(Departamento departamento)
    {
        if (ModelState.IsValid)
        {
            if (_departamentoService.Editar(departamento))
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
        var departamento = _departamentoService.ObtenerPorId(id);
        return View(departamento);
    }

    [HttpPost]
    public IActionResult Eliminar(Departamento departamento)
    {
        if (_departamentoService.Eliminar(departamento.Id))
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
