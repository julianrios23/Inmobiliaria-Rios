using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Inmobiliaria_Rios.Data;
using Inmobiliaria_Rios.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Rios.Controllers;

public class AccountController : Controller
{
    private readonly Inmobiliaria_Rios.Data.ApplicationDbContext _context;

    public AccountController(Inmobiliaria_Rios.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Buscar usuario por mail y contraseña y estado activo
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Mail == username && u.Contraseña == password && u.Estado == 1);

        if (usuario != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, (usuario.Nombre ?? "") + " " + (usuario.Apellido ?? "")),
                new Claim(ClaimTypes.Email, usuario.Mail ?? ""),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "empleado"),
                new Claim("UsuarioId", usuario.Idusuarios.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false // Esto asegura que la cookie no persista entre ejecuciones
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Redirigir al index general (Home/Index)
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Usuario o contraseña incorrectos o usuario inactivo";
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register(string username, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ModelState.AddModelError("PasswordMismatch", "Las contraseñas no coinciden.");
            return View();
        }

        // Aquí puedes agregar lógica para registrar al usuario
        TempData["Mensaje"] = "Registro exitoso. Ahora puedes iniciar sesión.";
        return RedirectToAction("Login", "Account");
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public IActionResult Perfil()
    {
        // El valor de User.Identity.Name es el nombre completo, pero tu base busca por Mail.
        // Recupera el mail del claim correspondiente:
        var userMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(userMail))
        {
            ViewBag.Error = "No se encontró el email del usuario logueado.";
            return View();
        }

        var usuario = _context.Usuarios.FirstOrDefault(u => u.Mail == userMail);
        if (usuario == null)
        {
            ViewBag.Error = "No se encontraron datos del usuario.";
            return View(); 
        }
        return View(usuario);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ActualizarImagenPerfil(int id, IFormFile imagenPerfil)
    {
        if (imagenPerfil != null && imagenPerfil.Length > 0)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Idusuarios == id);
            if (usuario != null)
            {
                var extension = Path.GetExtension(imagenPerfil.FileName);
                var nombreArchivo = $"perfil_{id}_{DateTime.Now.Ticks}{extension}";
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagenPerfil.CopyToAsync(stream);
                }

                // Aquí se guarda el nombre del archivo en la propiedad imgperfil del usuario.
                usuario.imgperfil = nombreArchivo;
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("Perfil");
    }

    [Authorize(Roles = "admin")]
    public IActionResult Perfiles()
    {
        var usuarios = _context.Usuarios.ToList();
        return View(usuarios);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult EditarPerfil(int Idusuarios, string Nombre, string Apellido, string Telefono, string Mail, string Rol, string Contraseña, string Estado)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Idusuarios == Idusuarios);
        if (usuario != null)
        {
            usuario.Nombre = Nombre;
            usuario.Apellido = Apellido;
            if (!string.IsNullOrWhiteSpace(Telefono) && long.TryParse(Telefono.Trim(), out var tel))
                usuario.Telefono = tel;
            else
                usuario.Telefono = null;
            usuario.Mail = Mail;
            usuario.Rol = Rol;
            usuario.Contraseña = Contraseña;
            if (!string.IsNullOrWhiteSpace(Estado) && int.TryParse(Estado, out var estadoInt))
                usuario.Estado = estadoInt;
            _context.Update(usuario);
            _context.SaveChanges();
        }
        return RedirectToAction("Perfiles");
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult CrearUsuario(string Nombre, string Apellido, string Telefono, string Mail, string Rol, string Contraseña)
    {
        long? tel = null;
        if (!string.IsNullOrWhiteSpace(Telefono))
        {
            var telLimpio = Telefono.Trim();
            if (long.TryParse(telLimpio, out var t))
                tel = t;
        }

        var usuario = new Usuario
        {
            Nombre = Nombre,
            Apellido = Apellido,
            Telefono = tel,
            Mail = Mail,
            Rol = Rol,
            Contraseña = Contraseña,
            Estado = 1 // Por defecto, activo
            // imgperfil queda null por defecto
        };
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return RedirectToAction("Perfiles");
    }
}
