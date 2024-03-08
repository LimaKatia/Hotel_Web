using CapaBL;
using CapaEN;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace UserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class UserController : Controller
    {
        UserBL userBL = new UserBL();
        RoleBL roleBL = new RoleBL();

        // GET:ACCION QUE MUESTRA LA LISTA DE USUARIOS REGISTRADOS
        public async Task<IActionResult> Index(UserEN user = null)
        {

            if (user == null)
                user = new UserEN();
            if (user.Top_Aux == 0)// si el usuario no a seleccionado cuantos quiere ver.
                user.Top_Aux = 10;//cantidad a mostrar por defecto.
            else if (user.Top_Aux == -1)//si es menor
                user.Top_Aux = 0; //se restet a 0

            var users = await userBL.SearchIncludeRoleAsync(user);
            var roles = await roleBL.GetAllAsync();

            ViewBag.Roles = roles;
            ViewBag.Top = user.Top_Aux;

            return View(users);
        }

        // GET: ACCION QUE MUESTRA EL DETALLE DE UN REGISTRO
        public async Task<IActionResult> Details(int id)
        {
            var user = await userBL.GetByIdAsync(new UserEN { Id = id });
            user.Role = await roleBL.GetByIdAsync(new RoleEN { Id = user.IdRole});

            return View(user);
        }

        // GET:ACCION QUE MUESTRA EL FORMULARIO PARA UN REGISTRO NUEVO
        //si es async por que la accion va a la base de datos
        public async Task<IActionResult> Create()
        {
            var roles = await roleBL.GetAllAsync();
            ViewBag.Roles = roles;

            return View();
        }

        // POST: ACCION QUE MUESTRA LOS DATOS Y LOS ENVIA A LA BD MEDIANTE EL MODELO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserEN user)
        {
            try
            {
                int result = await userBL.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await roleBL.GetAllAsync();

                return View(user);
            }
        }

        // GET:ACCION QUE MUESTRA EL FORMUKARIO CON LOS DATOS CARGADOS PARA MODIFICAR
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userBL.GetByIdAsync(new UserEN { Id = id });
            user.Role = await roleBL.GetByIdAsync(new RoleEN { Id = user.Id});
            ViewBag.Roles = await roleBL.GetAllAsync();

            return View(user);
        }

        // POST: ACCION QUE RECIBE LOS DATOS MODIFICADOS Y LOS ENVIA A LA BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserEN user)
        {
            try
            {
                int result = await userBL.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Roles = await roleBL.GetAllAsync();

                return View(user);
            }
        }

        // GET: ACCION QUE MUESTRA LOS DATOS PARA CONFIRMAR LA ELIMINIACION
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userBL.GetByIdAsync(new UserEN { Id = id });
            user.Role = await roleBL.GetByIdAsync(new RoleEN { Id = user.Id });

            return View(user);
        }

        // POST:ACCION QUEU RECIBE LA CONFIRMACION PARA ELIMINAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, UserEN user)
        {
            try
            {
                int result = await userBL.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                var userDb = await userBL.GetByIdAsync(user);
                if (userDb == null)
                    userDb = new UserEN();
                if (userDb.Id > 0)
                    userDb.Role = await roleBL.GetByIdAsync(new RoleEN { Id = userDb.Id });
                return View(userDb);
            }
        }

        //ACCION QUE MUESTRA EL FORMULARIO DE INICIO DE SECION
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //coobkies archivos que se guardan en la cache
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = returnUrl;
            ViewBag.Error = "";
            return View();
        }

        //ACCION QUE EJECUTA LA AUTENTICACION DEL USUARIO
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserEN user, string returnUrl = null)
        {
            try
            {
                var userDB = await userBL.LoginAsync(user);
                if (userDB != null && userDB.Id > 0 && userDB.Login == user.Login)
                {
                    userDB.Role = await roleBL.GetByIdAsync(new RoleEN { Id = userDB.Id });
                    //claim dentro de la seccion se tendra una propiedad Name esto a traves de Claims mientra este activa.
                    //claim son propiedades de la secion
                    var claims = new[] { new Claim(ClaimTypes.Name, userDB.Login), new Claim(ClaimTypes.Role, userDB.Role.Name) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                    throw new Exception("Hay un problema con sus credenciales");

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Url = returnUrl;
                ViewBag.Error = ex.Message;
                return View(new UserEN { Login = user.Login });
            }
        }
        //ACCION QUE PERMITE CERRAR LA SESION DEL USUARIO
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        //ACCION QUE MUESTRA EL FROMULARIO PARA CAMBIAR CONTRASEÑA
        public async Task<IActionResult> ChangePassword()
        {
            var users = await userBL.SearchAsync(new UserEN { Login = User.Identity.Name, Top_Aux = 1 });
            var acualUser = users.FirstOrDefault();
            return View(acualUser);
        }

        //ACCION QUE RESIBE LA CONTRASEÑA ACTUALISADA Y LA ENVIA A LA BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserEN user, string oldPassword)
        {
            try
            {
                int result = await userBL.ChangePasswordAsync(user, oldPassword);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "User");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                var users = await userBL.SearchAsync(new UserEN { Login = User.Identity.Name, Top_Aux = 1 });
                var acualUser = users.FirstOrDefault();
                return View(acualUser);
            }
        }
    }
}
