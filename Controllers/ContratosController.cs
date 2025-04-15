using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Inmobiliaria_Rios.Models; // Agrega este using para Contrato, Propiedad, Cliente

namespace Inmobiliaria_Rios.Controllers
{
    [Route("Contratos")]
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
            // Obtener lista de contratos desde la base de datos
            List<Contrato> contratos = new List<Contrato>();
            // ...cargar datos...
            return View(contratos);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                contrato.Estado = "Activo"; // Inicializar el miembro requerido

                // Cambiar estado del inmueble a no disponible (estado = 0)
                var inmueble = contexto.Propiedades.FirstOrDefault(p => p.Id == contrato.IdInmuebles);
                if (inmueble != null)
                {
                    inmueble.Estado = false; // Cambia el estado a false (no disponible)
                    contexto.Propiedades.Update(inmueble);
                }

                // Guardar contrato en la base de datos
                contexto.Contratos.Add(contrato);
                contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(contrato);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            // Obtener contrato por ID
            Contrato contrato = new Contrato { Estado = "Activo" };
            // ...cargar datos...
            return View(contrato);
        }

        [HttpPost("Edit")]
        public IActionResult Edit(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                contrato.Estado ??= "Activo"; // Asegurar que el miembro requerido esté inicializado
                // Actualizar contrato en la base de datos
                // ...actualizar datos...
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
