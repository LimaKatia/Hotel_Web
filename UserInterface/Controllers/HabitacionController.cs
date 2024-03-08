using CapaBL;
using CapaDAL;
using CapaEN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HabitacionController : Controller
    {
        HabitacionBL HabitacionBL = new HabitacionBL();

        EstadoBL estadoBL = new EstadoBL();
        TipoHabitacionBL tipoBL = new TipoHabitacionBL();

        public async Task<IActionResult> Index(HabitacionEN Habitacion = null)
        {
            if (Habitacion == null)
                Habitacion = new HabitacionEN();
            if (Habitacion.Top_Aux == 0)
                Habitacion.Top_Aux = 10;
            else if (Habitacion.Top_Aux == -1)
                Habitacion.Top_Aux = 0;

            var habitacion = await HabitacionBL.SearchAsync(Habitacion);
            ViewBag.Top = Habitacion.Top_Aux;
            return View(habitacion);
        }

        public async Task<IActionResult> Datils(int id)
        {
            var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            return View(Habitacion);
        }


        public async Task<IActionResult> Create()
        {
            try
            {
                // Obtine la lista de estados desde la base de datos
                List<EstadoEN> estados = await estadoBL.GetAllAsync();
                List<TipoHabitacionEN> tipo = await tipoBL.GetAllAsync();

                // Obtine la lista de estados desde la base de datos
                ViewBag.Estados = new SelectList(estados, "Id", "Nombre");
                ViewBag.Tipos = new SelectList(tipo, "Id", "Nombre");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HabitacionEN Habitacion)
        {
            try
            {
                int result = await HabitacionBL.CreateHabitacion(Habitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(Habitacion);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                List<EstadoEN> estados = await estadoBL.GetAllAsync();
                List<TipoHabitacionEN> tipo = await tipoBL.GetAllAsync();

                ViewBag.Estados = new SelectList(estados, "Id", "Nombre");
                ViewBag.Tipos = new SelectList(tipo, "Id", "Nombre");

                return View();
            }
            catch
            {
                var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
                ViewBag.Error = "";
                return View(Habitacion);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HabitacionEN Habitacion)
        {
            try
            {
                int result = await HabitacionBL.UpdateHabitacion(Habitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(Habitacion);
            }
        }

        ///////////////////////ELIMINAR////////////////////
        public async Task<IActionResult> Delete(int id)
        {
            var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            ViewBag.Error = "";
            return View(Habitacion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, HabitacionEN Habitacion)
        {
            try
            {
                int result = await HabitacionBL.DeleteHabitacion(Habitacion);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(Habitacion);
            }
        }
    }
}
