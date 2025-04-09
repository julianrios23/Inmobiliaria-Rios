using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Rios.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Aquí puedes agregar lógica de autenticación
        if (username == "admin" && password == "password")
        {
            TempData["Mensaje"] = "Inicio de sesión exitoso.";
            return RedirectToAction("Index", "Home");
        }

        TempData["Error"] = "Credenciales inválidas.";
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ModelState.AddModelError("PasswordMismatch", "Las contraseñas no coinciden.");
            return View();
        }

        // Aquí puedes agregar lógica para registrar al usuario
        TempData["Mensaje"] = "Registro exitoso. Ahora puedes iniciar sesión.";
        return RedirectToAction("Login");
    }
}
