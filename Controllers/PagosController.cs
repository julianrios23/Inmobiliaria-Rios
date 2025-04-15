using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Rios.Models;
using System.Collections.Generic;

namespace Inmobiliaria_Rios.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index(int contratoId)
        {
            // Obtener lista de pagos por contrato desde la base de datos
            List<Pago> pagos = new List<Pago>();
            // ...cargar datos...
            return View(pagos);
        }

        public IActionResult Create(int contratoId)
        {
            ViewBag.ContratoId = contratoId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pago pago)
        {
            if (ModelState.IsValid)
            {
                // Guardar pago en la base de datos
                // ...guardar datos...
                return RedirectToAction("Index", new { contratoId = pago.ContratoId });
            }
            return View(pago);
        }

        public IActionResult Delete(int id)
        {
            // Eliminar pago por ID
            // ...eliminar datos...
            return RedirectToAction("Index");
        }
    }
}
