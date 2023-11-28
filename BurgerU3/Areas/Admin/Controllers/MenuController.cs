using BurgerU3.Areas.Admin.Models.ViewModels;
using BurgerU3.Models.Entities;
using BurgerU3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BurgerU3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private Repository<Clasificacion> RepositorioClasif;
        private MenuRepository RepositorioM;

        public MenuController(Repository<Clasificacion> repositorioClasif, MenuRepository repositorioM)
        {
            this.RepositorioClasif = repositorioClasif;
            this.RepositorioM = repositorioM;
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Index(MenuAdminViewModel vm)
        {
            vm.ListaClasificaciones = RepositorioClasif.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionModel
            {
                Nombre = x.Nombre,
            }).ToList();

            vm.ListaH = RepositorioM.GetAll().OrderBy(x => x.Nombre).Select(x => new BurgerModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Precio = x.Precio,
                Descripcion = x.Descripción,
                Clasificacion = x.IdClasificacionNavigation.Nombre
            });



            return View(vm);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Agregar()
        //{
        //    return View();
        //}
    }
}
