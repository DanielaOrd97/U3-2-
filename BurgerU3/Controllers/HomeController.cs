using BurgerU3.Models.Entities;
using BurgerU3.Models.ViewModels;
using BurgerU3.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace BurgerU3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClasifRepository ClasifRepo;

        //private readonly Repository<Clasificacion> ClasifRepo;
        //private readonly MenuRepository MenuRepo;

        //public HomeController(Repository<Clasificacion> clasifRepo, MenuRepository menuRepo)
        //{
        //    this.ClasifRepo = clasifRepo;
        //    this.MenuRepo = menuRepo;
        //}

        public HomeController(ClasifRepository clasifRepo)
        {
            this.ClasifRepo = clasifRepo;   
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerMenu()
        {
            MenuViewModel vm = new();

            vm.ListaClasificaciones = ClasifRepo.GetAll().OrderBy(x => x.Nombre);

            return View(vm);
        }
    }
}
