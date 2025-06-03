using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Inmobiliaria_Rios.Models; // Agrega este using para Contrato, Propiedad, Cliente
using System;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria_Rios.Controllers
{
    [Route("Contratos")]
    [Authorize]
    public class ContratosController : Controller
    {
        private readonly Inmobiliaria_Rios.Data.ApplicationDbContext contexto;

        public ContratosController(Inmobiliaria_Rios.Data.ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var contratos = contexto.Contratos.ToList();
            ViewBag.Clientes = contexto.Clientes.ToList();
            ViewBag.Inmuebles = contexto.Propiedades.ToList();
            // Agregar la lista de usuarios para mostrar nombre y apellido en la vista
            ViewBag.Usuarios = contexto.Usuarios.ToList();
            return View(contratos);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            // Redirigir a la vista de nuevo contrato con combos cargados
            var inmuebles = contexto.Propiedades.ToList();
            var clientes = contexto.Clientes.ToList();
            ViewBag.Inmuebles = inmuebles;
            ViewBag.Clientes = clientes;
            return View("NuevoContrato");
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                // Asigna el ID del usuario logueado (debe existir en la tabla usuarios)
                // Si tienes el ID en las Claims:
                // contrato.UsuarioCreacionId = int.Parse(User.FindFirst("Idusuarios").Value);

                // Si no tienes el ID en las Claims, busca el usuario por nombre:
                // Evitar posible null en User.Identity
                string? userName = User?.Identity?.Name;
                var usuario = contexto.Usuarios.FirstOrDefault(u =>
                    u.Nombre == userName ||
                    u.Mail == userName ||
                    (u.Nombre + " " + u.Apellido) == userName
                );
                if (usuario != null)
                {
                    contrato.UsuarioCreacionId = usuario.Idusuarios;
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo identificar el usuario actual para asignar como creador del contrato. Verifique que el usuario esté correctamente registrado y que el nombre de usuario coincida con el campo 'Nombre', 'Mail' o 'Nombre Apellido' en la tabla usuarios.");
                    ViewBag.Inmuebles = contexto.Propiedades.ToList();
                    ViewBag.Clientes = contexto.Clientes.ToList();
                    return View("NuevoContrato", contrato);
                }

                contrato.Estado = true;
                contrato.FechaCreacion = DateTime.Now;
                // Las fechas de inicio y fin ya vienen del formulario
                // contrato.FechaInicio y contrato.FechaFin

                try
                {
                    contexto.Contratos.Add(contrato);

                    // Cambiar el estado del inmueble a 0 (inactivo/no disponible)
                    var inmueble = contexto.Propiedades.FirstOrDefault(p => p.Id == contrato.IdInmuebles);
                    if (inmueble != null)
                    {
                        inmueble.Estado = false;
                    }

                    contexto.SaveChanges();

                    TempData["Mensaje"] = "Contrato guardado correctamente.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Mostrar el error en la vista para depuración
                    ModelState.AddModelError("", "Error al guardar el contrato: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", "Detalle: " + ex.InnerException.Message);
                        System.Diagnostics.Debug.WriteLine("InnerException: " + ex.InnerException.Message);
                    }
                    ViewBag.Inmuebles = contexto.Propiedades.ToList();
                    ViewBag.Clientes = contexto.Clientes.ToList();
                    System.Diagnostics.Debug.WriteLine("Excepción al guardar: " + ex.Message);
                    return View("NuevoContrato", contrato);
                }
            }
            ViewBag.Inmuebles = contexto.Propiedades.ToList();
            ViewBag.Clientes = contexto.Clientes.ToList();
            return View(contrato);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            // Obtener contrato por ID
            var contrato = contexto.Contratos.FirstOrDefault(c => c.Id == id);
            if (contrato == null)
            {
                return NotFound();
            }
            // No asignar string a Estado, ya es bool
            return View(contrato);
        }

        [HttpPost("Edit")]
        public IActionResult Edit(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                var contratoOriginal = contexto.Contratos.FirstOrDefault(c => c.Id == contrato.Id);
                if (contratoOriginal != null)
                {
                    contratoOriginal.IdInmuebles = contrato.IdInmuebles;
                    contratoOriginal.IdClientes = contrato.IdClientes;
                    contratoOriginal.FechaInicio = contrato.FechaInicio;
                    contratoOriginal.FechaFin = contrato.FechaFin;
                    contratoOriginal.FechaFinAnticipada = contrato.FechaFinAnticipada;
                    contratoOriginal.MontoMensual = contrato.MontoMensual;
                    contratoOriginal.Multa = contrato.Multa;
                    contratoOriginal.Estado = contrato.Estado;

                    // Validar si la fecha actual es mayor a la fecha de finalización
                    if (DateTime.Now.Date > contratoOriginal.FechaFin.Date)
                    {
                        contratoOriginal.Estado = false; // Inactivo

                        // Cambiar el estado del inmueble a disponible (true)
                        var inmueble = contexto.Propiedades.FirstOrDefault(p => p.Id == contratoOriginal.IdInmuebles);
                        if (inmueble != null)
                        {
                            inmueble.Estado = true;
                        }
                    }

                    // Guardar el id del usuario logueado en UsuarioModificacionId
                    var userName = User?.Identity?.Name ?? "";
                    var usuario = contexto.Usuarios.FirstOrDefault(u =>
                        u.Nombre == userName ||
                        u.Mail == userName ||
                        (u.Nombre + " " + u.Apellido) == userName
                    );
                    if (usuario != null)
                    {
                        contratoOriginal.UsuarioModificacionId = usuario.Idusuarios;
                    }

                    // Guardar la fecha de modificación
                    contratoOriginal.FechaModificacion = DateTime.Now;

                    contexto.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(contrato);
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            // Eliminar contrato por ID
            // ...eliminar datos...
            return RedirectToAction("Index");
        }

        [HttpGet("NuevoContrato")]
        public IActionResult NuevoContrato()
        {
            var inmuebles = contexto.Propiedades.ToList() ?? new List<Propiedad>();
            var clientes = contexto.Clientes.ToList() ?? new List<Cliente>();
            ViewBag.Inmuebles = inmuebles;
            ViewBag.Clientes = clientes;
            return View();
        }

        [HttpGet("EditarContrato/{id}")]
        public IActionResult EditarContrato(int id)
        {
            var contrato = contexto.Contratos.FirstOrDefault(c => c.Id == id);
            if (contrato == null)
                return NotFound();

            ViewBag.Inmuebles = contexto.Propiedades.ToList();
            ViewBag.Clientes = contexto.Clientes.ToList();
            ViewBag.Usuarios = contexto.Usuarios.ToList();
            return View("EditarContrato", contrato);
        }
    }
}
