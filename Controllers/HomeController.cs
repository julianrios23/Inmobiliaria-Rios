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
        _context = context; // Verifique que el contexto esté correctamente inyectado
    }

    public IActionResult Index()
    {
        return View(); // Restaurar para que cargue la vista Index
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Propiedades()
    {
        var propiedades = _context.Propiedades
            .Include(p => p.Propietario) // Verifique que la relación 'Propietario' esté configurada en el modelo
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
        var clientes = _context.Clientes.ToList(); // Verifique que la tabla 'Clientes' exista en la base de datos
        return View(clientes); // Envía la lista de clientes a la vista
    }

    public IActionResult Propietarios()
    {
        var propietarios = _context.Propietarios
            .Where(p => p.Estado == true) // Filtrar solo propietarios activos
            .ToList();

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
            // cuit = propietario.CUIT, // Eliminar esta línea si no es necesaria
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

    public IActionResult Contacto()
    {
        return View();
    }

    public IActionResult EditarCliente(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id); // Recupera el cliente por su ID
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente); // Envía el cliente a la vista
    }

    [HttpPost]
    public IActionResult EditarCliente(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == cliente.Id); // Cambiado IdCliente a Id
                if (clienteExistente == null)
                {
                    TempData["Error"] = "El cliente no existe.";
                    return RedirectToAction("Clientes");
                }

                // Actualizar los campos editables
                clienteExistente.Nombre = cliente.Nombre;
                clienteExistente.Apellido = cliente.Apellido;
                clienteExistente.DNI = cliente.DNI;
                clienteExistente.Email = cliente.Email; 
                clienteExistente.Telefono = cliente.Telefono;
                clienteExistente.Domicilio = cliente.Domicilio;
                clienteExistente.Localidad = cliente.Localidad;
                clienteExistente.Provincia = cliente.Provincia;

                _context.SaveChanges();
                TempData["Mensaje"] = "Cliente actualizado exitosamente"; // Mensaje consistente con propietarios
                return RedirectToAction("Clientes");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el cliente: " + ex.Message);
            }
        }

        TempData["Error"] = "Error de validación: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        return View(cliente);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
