using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Rios.Models;
using ApplicationDbContextAlias = Inmobiliaria_Rios.Data.ApplicationDbContext;  // Alias para resolver la ambigüedad
using Microsoft.EntityFrameworkCore; // Importación necesaria para Include
using System.Collections.Generic;

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
        var propiedades = _context.Propiedades
            .Include(p => p.Propietario) // Asegúrate de incluir la relación con Propietario
            .Where(p => p.Estado) // Filtra solo las propiedades activas
            .ToList();

        return View(propiedades);
    }

    public IActionResult NuevoInmueble()
    {
        var propietariosActivos = _context.Propietarios.Where(p => p.Estado == true).ToList();
        ViewBag.Propietarios = propietariosActivos;
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

    [HttpPost]
    public IActionResult GuardarInmueble(Propiedad propiedad)
    {
        if (propiedad.IdPropietario == 0)
        {
            ModelState.AddModelError("IdPropietario", "Debe seleccionar un propietario válido.");
        }

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Error de validación: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View("NuevoInmueble", propiedad);
        }

        try
        {
            propiedad.Estado = true;
            _context.Propiedades.Add(propiedad);
            _context.SaveChanges();
            TempData["Mensaje"] = "Propiedad guardada exitosamente";
            return RedirectToAction("Propiedades");
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Error al guardar la propiedad: " + ex.Message;
            return View("NuevoInmueble", propiedad);
        }
    }

    public IActionResult EditarInmueble(int id)
    {
        var propiedad = _context.Propiedades
            .Include(p => p.Propietario) // Incluir datos del propietario
            .FirstOrDefault(p => p.Id == id);

        if (propiedad == null)
        {
            return NotFound();
        }

        ViewBag.Propietarios = _context.Propietarios.Where(p => p.Estado).ToList();
        return View(propiedad); // Enviar toda la información de la propiedad al modelo
    }

    [HttpPost]
    public IActionResult EditarInmueble(Propiedad propiedad)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var propiedadExistente = _context.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
                if (propiedadExistente == null)
                {
                    TempData["Error"] = "La propiedad no existe.";
                    return RedirectToAction("Propiedades");
                }

                // Actualizar solo los campos editables
                propiedadExistente.Precio = propiedad.Precio;
                propiedadExistente.Opcion = propiedad.Opcion;
                propiedadExistente.Observaciones = propiedad.Observaciones;

                _context.SaveChanges();
                TempData["Mensaje"] = "Propiedad actualizada exitosamente.";
                return RedirectToAction("Propiedades");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar la propiedad: " + ex.Message);
            }
        }

        TempData["Error"] = "Error de validación: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        return View(propiedad);
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
                ModelState.AddModelError("Dni", "El Dni ingresado ya está registrado.");
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

    [HttpGet]
    public IActionResult GetPropietario(int id)
    {
        var propietario = _context.Propietarios.Find(id);
        if (propietario == null)
        {
            return NotFound();
        }

        return Json(new
        {
            nombre = propietario.Nombre,
            apellido = propietario.Apellido,
            dni = propietario.DNI, // Cambiado de Dni a DNI
            cuit = propietario.Cuit,
            telefono = propietario.Telefono,
            mail = propietario.Mail,
            domicilio = propietario.Domicilio,
            localidad = propietario.Localidad,
            provincia = propietario.Provincia
        });
    }

    [HttpGet]
    public IActionResult BuscarPropietarioPorDNI(string dni)
    {
        if (int.TryParse(dni, out int dniNumero))
        {
            var propietario = _context.Propietarios
                .Where(p => p.DNI == dniNumero && p.Estado) // Cambiado de Dni a DNI
                .FirstOrDefault();

            if (propietario == null)
                return Json(null);

            return Json(new {
                id = propietario.Id,
                nombre = propietario.Nombre,
                apellido = propietario.Apellido,
                dni = propietario.DNI // Cambiado de Dni a DNI
            });
        }
        return Json(null);
    }

    [HttpGet]
    public IActionResult BuscarPropietario(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Json(new { success = false, message = "Ingrese un término de búsqueda válido." });
        }

        var propietarios = _context.Propietarios
            .Where(p => p.Estado && 
                        (p.Nombre.Contains(searchTerm) || p.Apellido.Contains(searchTerm)))
            .Select(p => new {
                id = p.Id,
                nombre = p.Nombre,
                apellido = p.Apellido,
                dni = p.DNI, // Cambiado de Dni a DNI
                texto = $"{p.Nombre} {p.Apellido} (DNI: {p.DNI})" // Cambiado de Dni a DNI
            })
            .Take(10)
            .ToList();

        return Json(new { success = true, results = propietarios });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
