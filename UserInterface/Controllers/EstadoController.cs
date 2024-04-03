using CapaBL;
using CapaDAL;
using CapaEN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class EstadoController : Controller
    {
        EstadoBL estadoBL = new EstadoBL();
        public async Task<IActionResult> Index(EstadoEN Estado = null)
        {
            if (Estado == null)
                Estado = new EstadoEN();
            if (Estado.Top_Aux == 0)
                Estado.Top_Aux = 10;
            else if (Estado.Top_Aux == -1)
                Estado.Top_Aux = 0;

            var estados = await estadoBL.SearchAsync(Estado);
            ViewBag.Top = Estado.Top_Aux;
            return View(estados);
        }
        public async Task<IActionResult> Datils(int id)
        {
            var Estado = await estadoBL.GetEstadoAsync(new EstadoEN { Id = id});
            return View(Estado);
        }

        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Insertar([FromBody] EstadoEN modelo)
        {
            EstadoEN NuevoModelo = new EstadoEN()
            {
                Nombre = modelo.Nombre,

            };
            int respuesta = await EstadoBL.CreateState(NuevoModelo);
            TempData["AlertaMessage"] = "Estado guardado exitosamente";
            return Json(new { valor = respuesta });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Estado = await estadoBL.GetEstadoAsync(new EstadoEN { Id = id });
            ViewBag.Error = "";
            return View(Estado);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EstadoEN Estado)
        {
            try
            {
                int result = await estadoBL.UpdateEstado(Estado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(Estado);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Estado = await estadoBL.GetEstadoAsync(new EstadoEN { Id = id });
            ViewBag.Error = "";
            return View(Estado);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EstadoEN estado)
        {
            try
            {
                int result = await estadoBL.DeleteEstado(estado);
                TempData["AlertMessage"] = "Estado eliminado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(estado);

            }
        }
    }
}
