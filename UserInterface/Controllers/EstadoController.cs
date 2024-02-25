using CapaBL;
using CapaDAL;
using CapaEN;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class EstadoController : Controller
    {
        EstadoBL estado = new EstadoBL();   
        public IActionResult Index()
        {
            return View();
        }
    }
}
