using CapaEN;
using CapaBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace UserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class RoleController : Controller
    {
    RoleBL roleBL = new RoleBL();

        // GET: CategoryController
        //ACCION QUE MUESTRA EL LISTADO DE CATEGORIAS
        public async Task<IActionResult> Index(RoleEN role = null)
        {
            if(role == null)
                role = new RoleEN();
            if(role.Top_Aux == 0)// si el usuario no a seleccionado cuantos quiere ver.
                role.Top_Aux = 10;//cantidad a mostrar por defecto.
            else if(role.Top_Aux == -1)//si es menor
                    role.Top_Aux = 0; //se restet a 0

            var roles = await roleBL.SearchAsync(role);
            ViewBag.Top = role.Top_Aux;

            return View(roles);
        }

        // GET: CategoryController/Details/5
        //// Accion que muestra el detalle de un registro
        public async Task<IActionResult> Details(int id)
        {
            var role = await roleBL.GetByIdAsync(new RoleEN { Id = id });
            return View(role);
        }

        // GET: CategoryController/Create
        // accion que muestra el formulario para crear una nuevo role
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();

            //return View();
        }

        // POST: CategoryController/Create
        // Accion que recibe los datos y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleEN role)
        {
            try
            {
                int result = await roleBL.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var role = await roleBL.GetByIdAsync(new RoleEN { Id = id });
            ViewBag.Error = "";
            return View(role);

        }

        // POST: CategoryController/Edit/5
        // accion que muestra el formulario para modifcar datos con ayuda
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,RoleEN role)
        {
            try
            {
                int result = await roleBL.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }

        // GET: CategoryController/Delete/5
        //accion que muestra los datos para confirmar la eliminacion
        public async Task<IActionResult> Delete(int id)
        {
            var role = await roleBL.GetByIdAsync(new RoleEN { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // POST: CategoryController/Delete/5
        //Accion que recibe la informacion para validar la eliminacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, RoleEN role)
        {
            try
            {
                int result = await roleBL.DeleteAsync(role);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }
    }
}
