using CapaBL;
using CapaDAL;
using CapaEN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UserInterface.Controllers
{
    public class HabitacionController : Controller
    {
        HabitacionBL HabitacionBL = new HabitacionBL();
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

        public async Task<IActionResult> Datils(byte id)
        {
            var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id});
            return View(Habitacion);
        }
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
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


        public async Task<IActionResult> Edit(byte id)
        {
            var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            ViewBag.Error = "";
            return View(Habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(byte id, HabitacionEN Habitacion)
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
        public async Task<IActionResult> Delete(byte id)
        {
            var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            ViewBag.Error = "";
            return View(Habitacion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(byte id, HabitacionEN Habitacion)
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
