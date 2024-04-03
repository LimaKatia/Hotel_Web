using CapaBL;
using CapaEN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using UserInterface.Helpers;

namespace UserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador, Cliente")]

    public class SalonController : Controller
    {
        SalonBL SalonBL = new SalonBL();
        EstadoBL estadoBL = new EstadoBL();
        TipoDeSalonBL tipodesalonBL = new TipoDeSalonBL();
        ImagenSalonBL imageBL = new ImagenSalonBL();

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(SalonEN salon = null)
        {
            if (salon == null)
                salon = new SalonEN();
            if (salon.Top_Aux == 0)
                salon.Top_Aux = 10;
            else if (salon.Top_Aux == -1)
                salon.Top_Aux = 0;

            var salones = await SalonBL.SearchAsync(salon);
            ViewBag.Top = salon.Top_Aux;
            return View(salones);
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> ViewSalon(SalonEN salon = null)
        {
            if (salon == null)
                salon = new SalonEN();
            if (salon.Top_Aux == 0)
                salon.Top_Aux = 10;
            else if (salon.Top_Aux == -1)
                salon.Top_Aux = 0;

            var salones = await SalonBL.SearchAsync(salon);
            ViewBag.Top = salon.Top_Aux;
            return View(salones);
        }

        // GET: SalonController/Details/5

        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Datils(int id)
        {
            var salon = await SalonBL.GetSalonAsync(new SalonEN { Id = id });
            salon.image = await imageBL.SearchAsync(new ImagenSalonEN() { IdSalon = salon.Id });
            return View(salon);
        }

        // GET: SalonController/Create
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create()
        {
            var estados = await estadoBL.GetAllAsync();
            var tipos = await tipodesalonBL.GetAllAsync();

            ViewBag.Estado = estados;
            ViewBag.tipo = tipos;
            return View();
        }

        // POST: SalonController/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SalonEN salon, List<IFormFile> formFiles)
        {
            try
            {
                //Declaración de la lista para almacenar las imágenes
                List<ImagenSalonEN> images = new List<ImagenSalonEN>();
                //Recorremos en caso que venga dos o más imágenes
                foreach (IFormFile file in formFiles)
                {
                    ImagenSalonEN imagen = new ImagenSalonEN();
                    imagen.IdSalon = salon.Id;
                    imagen.UrlImage = await ImgHelpers.SubirArchivo(file.OpenReadStream(), file.FileName);
                    images.Add(imagen);
                }

                salon.image = images;
                int result = await SalonBL.CreateSalon(salon);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Estado = await estadoBL.GetAllAsync();
                ViewBag.Tipo = await tipodesalonBL.GetAllAsync();
                return View(salon);
            }
        }

        // GET: SalonController/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            List<EstadoEN> estados = await estadoBL.GetAllAsync();
            List<TipoDeSalonEN> tipodesalon = await tipodesalonBL.GetAllAsync();
            var Salon = await SalonBL.GetSalonAsync(new SalonEN { Id = id });
            ViewBag.Error = "";

            ViewBag.Estados = new SelectList(estados, "Id", "Nombre");
            ViewBag.Tipos = new SelectList(tipodesalon, "Id", "Nombre");
            return View(Salon);
        }

        // POST: SalonController/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalonEN Salon)
        {
            try
            {
                int result = await SalonBL.UpdateHabitacion(Salon);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(Salon);
            }
        }

        // GET: SalonController/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var Salon = await SalonBL.GetSalonAsync(new SalonEN { Id = id });
            ViewBag.Error = "";
            return View(Salon);
        }

        // POST: SalonController/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, SalonEN salon)
        {
            try
            {
                SalonEN salonDB = await SalonBL.GetSalonAsync(salon);
                salonDB.image = await imageBL.SearchAsync(new ImagenSalonEN() { IdSalon = salon.Id });
                if(salonDB.image.Count() > 0)
                {
                    foreach(var images in salonDB.image)
                    {
                        await imageBL.DeleteAsync(images);
                    }
                }

                int result = await SalonBL.DeleteSalon(salon);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(salon);
            }
        }
    }
}
