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

        public MenuRepository MenuRepo { get; }

        //private readonly Repository<Clasificacion> ClasifRepo;
        //private readonly menurepository menurepo;

        //public HomeController(Repository<Clasificacion> clasifRepo, MenuRepository menuRepo)
        //{
        //    this.ClasifRepo = clasifRepo;
        //    this.MenuRepo = menuRepo;
        //}

        public HomeController(ClasifRepository clasifRepo, MenuRepository menuRepo)
        {
            this.ClasifRepo = clasifRepo;
            this.MenuRepo = menuRepo;
        }


        public IActionResult Index()
        {
            return View();
        }

      

        public IActionResult VerMenu(string? id)
        {
            MenuViewModel vm = new();

            vm.ListaClasificaciones = ClasifRepo.GetAll().OrderBy(x => x.Nombre);

            if(id != null)
            {

                id = id.Replace("-", " ");

                var hamburguesa = MenuRepo.GetByNombre(id);

                vm.IdSeleccionado = hamburguesa.Id;
                vm.Descripcion = hamburguesa.Descripción;
            }

            return View(vm);
        }


        public IActionResult VerPromociones()
        {
            PromocionesViewModel vm = new();
            vm.ListaPromos = new List<Menu>();

            foreach (var item in MenuRepo.GetConPromociones())
            {
                vm.ListaPromos.Add(item);
            }


            return View(vm);
        }
    }
}
