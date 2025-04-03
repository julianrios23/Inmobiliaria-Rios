using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Rios.Models;
using ApplicationDbContextAlias = Inmobiliaria_Rios.Data.ApplicationDbContext;  // Alias para resolver la ambigüedad

namespace Inmobiliaria_Rios.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContextAlias _context;

    public HomeController(ApplicationDbContextAlias context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var propiedades = _context.Propiedades.ToList();
        return View(propiedades);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Propiedades()
    {
        var propiedades = _context.Propiedades.Where(p => p.Estado).ToList();
        return View(propiedades);
    }

    public IActionResult NuevoInmueble()
    {
        return View();
    }

    [HttpPost]
    public IActionResult NuevoInmueble(Propiedad propiedad)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Propiedades.Add(propiedad);
                _context.SaveChanges();
                TempData["Mensaje"] = "Propiedad guardada exitosamente";
                return RedirectToAction("Propiedades");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar la propiedad: " + ex.Message);
            }
        }
        return View(propiedad);
    }

    public IActionResult EditarInmueble(int id)
    {
        var propiedad = _context.Propiedades.Find(id);
        if (propiedad == null)
        {
            return NotFound();
        }
        return View("NuevoInmueble", propiedad);
    }

    [HttpPost]
    public IActionResult EditarInmueble(Propiedad propiedad)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(propiedad);
                _context.SaveChanges();
                TempData["Mensaje"] = "Propiedad actualizada exitosamente";
                return RedirectToAction("Propiedades");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar la propiedad: " + ex.Message);
            }
        }
        return View("NuevoInmueble", propiedad);
    }

    public IActionResult Clientes()
    {
        var clientes = _context.Clientes.ToList(); // Recupera todos los clientes de la BD
        return View(clientes); // Envía la lista de clientes a la vista
    }

    public IActionResult Propietarios()
    {
        var propietarios = _context.Propietarios.ToList(); // Recupera todos los propietarios de la BD
        return View(propietarios);
    }

    public IActionResult NuevoPropietario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult NuevoPropietario(Propietario propietario)
    {
        if (ModelState.IsValid)
        {
            if (_context.Propietarios.Any(p => p.DNI == propietario.DNI))
            {
                ModelState.AddModelError("DNI", "El DNI ingresado ya está registrado.");
                return View(propietario);
            }

            try
            {
                _context.Propietarios.Add(propietario);
                _context.SaveChanges();
                TempData["Mensaje"] = "Propietario guardado exitosamente";
                return RedirectToAction("Propietarios");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el propietario: " + ex.Message);
            }
        }
        return View(propietario);
    }

    public IActionResult EditarPropietario(int id)
    {
        var propietario = _context.Propietarios.Find(id);
        if (propietario == null)
        {
            return NotFound();
        }
        return View(propietario);
    }

    [HttpPost]
    public IActionResult EditarPropietario(Propietario propietario)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(propietario);
                _context.SaveChanges();
                TempData["Mensaje"] = "Propietario actualizado exitosamente";
                return RedirectToAction("Propietarios");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el propietario: " + ex.Message);
            }
        }
        return View(propietario);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
