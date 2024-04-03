using CapaBL;
using CapaDAL;
using CapaEN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserInterface.Helpers;

namespace UserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador, Cliente")]

    public class HabitacionController : Controller
    {
        HabitacionBL HabitacionBL = new HabitacionBL();
        EstadoBL estadoBL = new EstadoBL();
        TipoHabitacionBL tipoBL = new TipoHabitacionBL();
        ImageBL imageBL = new ImageBL();

        [Authorize(Roles = "Administrador")]
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
        public async Task<IActionResult> ViewUser(HabitacionEN Habitacion = null)
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


        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Datils(int id)
        {
            var habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            habitacion.image = await imageBL.SearchAsync(new ImageEN() { IdHabitacion = habitacion.Id });
            return View(habitacion);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create()
        {
            var estados = await estadoBL.GetAllAsync();
            var tipos = await tipoBL.GetAllAsync();

            ViewBag.Estado = estados;
            ViewBag.Tipo = tipos;
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HabitacionEN habitacion, List<IFormFile> formFiles)
        {
            try
            {
           //Declaración de la lista para almacenar las imágenes
                List<ImageEN> images = new List<ImageEN>();
           //Recorremos en caso que venga dos o más imágenes
                foreach (IFormFile file in formFiles)
                {
                    ImageEN imagen = new ImageEN();
                    imagen.IdHabitacion = habitacion.Id;
                    imagen.UrlImage = await ImgHelpers.SubirArchivo(file.OpenReadStream(), file.FileName);
                    images.Add(imagen);
                }

                habitacion.image = images;
                int result = await HabitacionBL.CreateHabitacion(habitacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Estado = await estadoBL.GetAllAsync();
                ViewBag.Tipo = await tipoBL.GetAllAsync();
                return View(habitacion);
            }
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {

            List<EstadoEN> estado = await estadoBL.GetAllAsync();
            List<TipoHabitacionEN> tipo = await tipoBL.GetAllAsync();
            var estados = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            ViewBag.Error = "";

            ViewBag.Estados = new SelectList(estado, "Id", "Nombre");
            ViewBag.Tipos = new SelectList(tipo, "Id", "Nombre");
            return View(estados);
        }

        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var Habitacion = await HabitacionBL.GetHabitacionAsync(new HabitacionEN { Id = id });
            ViewBag.Error = "";
            return View(Habitacion);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, HabitacionEN habitacion)
        {
            try
            {
                HabitacionEN habitacionDB = await HabitacionBL.GetHabitacionAsync(habitacion);
                habitacionDB.image = await imageBL.SearchAsync(new ImageEN() { IdHabitacion = habitacionDB.Id });
                if (habitacionDB.image.Count() > 0)
                {
                    foreach (var images in habitacionDB.image)
                    {
                        await imageBL.DeleteAsync(images);
                    }
                }
                int result = await HabitacionBL.DeleteHabitacion(habitacion);
                    return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(habitacion);
            }
        }
    }
}
