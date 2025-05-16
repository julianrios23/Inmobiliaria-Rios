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
            // Log temporal para depuración
            System.Diagnostics.Debug.WriteLine("POST Contrato:");
            System.Diagnostics.Debug.WriteLine($"IdInmuebles: {contrato.IdInmuebles}");
            System.Diagnostics.Debug.WriteLine($"IdClientes: {contrato.IdClientes}");
            System.Diagnostics.Debug.WriteLine($"FechaInicio: {contrato.FechaInicio}");
            System.Diagnostics.Debug.WriteLine($"FechaFin: {contrato.FechaFin}");
            System.Diagnostics.Debug.WriteLine($"MontoMensual: {contrato.MontoMensual}");

            if (!ModelState.IsValid || contrato.IdInmuebles == 0 || contrato.IdClientes == 0)
            {
                if (contrato.IdInmuebles == 0)
                    ModelState.AddModelError("IdInmuebles", "Debe seleccionar un inmueble.");
                if (contrato.IdClientes == 0)
                    ModelState.AddModelError("IdClientes", "Debe seleccionar un cliente.");

                ViewBag.Inmuebles = contexto.Propiedades.ToList();
                ViewBag.Clientes = contexto.Clientes.ToList();

                // Mostrar errores de validación en la vista
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine("ModelState error: " + error.ErrorMessage);
                }

                return View("NuevoContrato", contrato);
            }

            try
            {
                contrato.FechaCreacion = DateTime.Now;
                contrato.Estado = true; // Booleano, se guarda como 1 en la base de datos

                // Solución: asignar un valor por defecto a UsuarioCreacionId si existe en el modelo
                var propUsuarioCreacion = contrato.GetType().GetProperty("UsuarioCreacionId");
                if (propUsuarioCreacion != null && propUsuarioCreacion.PropertyType == typeof(int))
                {
                    // Puedes poner aquí el ID del usuario actual si tienes autenticación, o un valor fijo temporal
                    propUsuarioCreacion.SetValue(contrato, 1); // 1 es un ejemplo, usa el valor adecuado
                }

                contexto.Contratos.Add(contrato);
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
                // Buscar el contrato original en la base de datos
                var contratoOriginal = contexto.Contratos.FirstOrDefault(c => c.Id == contrato.Id);
                if (contratoOriginal != null)
                {
                    contratoOriginal.IdInmuebles = contrato.IdInmuebles;
                    contratoOriginal.IdClientes = contrato.IdClientes;
                    contratoOriginal.FechaInicio = contrato.FechaInicio;
                    contratoOriginal.FechaFin = contrato.FechaFin;
                    contratoOriginal.MontoMensual = contrato.MontoMensual;
                    
                    contratoOriginal.Estado = contrato.Estado;

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
    }
}
