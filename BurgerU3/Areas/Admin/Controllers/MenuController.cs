using BurgerU3.Areas.Admin.Models.ViewModels;
using BurgerU3.Models.Entities;
using BurgerU3.Repositories;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BurgerU3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly ClasifRepository RepoClasif;
        private MenuRepository RepoMenu;

        public MenuController(ClasifRepository repoClasif, MenuRepository repoMenu)
        {
            this.RepoClasif = repoClasif;
            this.RepoMenu = repoMenu;   
        }

        [HttpGet]
        [HttpPost]

        public IActionResult Index(MenuAdminViewModel vm)
        {

            vm.ListaClasificaciones = RepoClasif.GetAll().OrderBy(x => x.Nombre);

            return View(vm);
        }

        public IActionResult Agregar()
        {
            AgregarElementoAdminViewModel vm = new();
            vm.ListaClasificaciones = RepoClasif.GetAll().OrderBy(x => x.Nombre);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(AgregarElementoAdminViewModel vm)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(vm.menu.Nombre))
            {
                ModelState.AddModelError("", "escriba el nombre correspondiente.");
            }
            if (vm.menu.Precio == null || vm.menu.Precio <= 0)
            {
                ModelState.AddModelError("", "introduzca el precio correctamente.");
            }
            if (string.IsNullOrWhiteSpace(vm.menu.Descripción))
            {
                ModelState.AddModelError("", "ingrese la descripcion.");
            }

            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "solo se permiten imagenes png.");
                }

                if (vm.Archivo.Length > 500 * 1024)//500kb
                {
                    ModelState.AddModelError("", "solo se permiten archivos no mayores a 500kb");

                }
            }

            if (ModelState.IsValid)
            {
                RepoMenu.Insert(vm.menu);

                if (vm.Archivo == null)
                {
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{vm.menu.Id}.png");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.menu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }

            vm.ListaClasificaciones = RepoClasif.GetAll().OrderBy(x => x.Nombre);
            return View(vm);
        }




        public IActionResult Editar(int id)
        {
            var hamburguesa = RepoMenu.Get(id);

            if(hamburguesa == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AgregarElementoAdminViewModel vm = new();
                vm.menu = hamburguesa;
                vm.ListaClasificaciones = RepoClasif.GetAll().OrderBy(x => x.Nombre);
                return View(vm);

            }
        }


        [HttpPost]
        public IActionResult Editar(AgregarElementoAdminViewModel vm)
        {

            ModelState.Clear();

            if (vm.Archivo != null) //Si selecciono un archivo
            {
                //MIME TYPE
                if (vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes JPG.");
                }

                if (vm.Archivo.Length > 500 * 1024)//500kb
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");

                }
            }

            if (ModelState.IsValid)
            {
                var hamburguesa = RepoMenu.Get(vm.menu.Id);
                if (hamburguesa == null)
                {
                    return RedirectToAction("Index");
                }

                hamburguesa.Nombre = vm.menu.Nombre;
                hamburguesa.Precio = vm.menu.Precio;
                hamburguesa.Descripción = vm.menu.Descripción;
                hamburguesa.IdClasificacion = vm.menu.IdClasificacion;
                
                RepoMenu.Update(hamburguesa);

                //Editar la foto
                if (vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.menu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }

            vm.ListaClasificaciones = RepoClasif.GetAll().OrderBy(x => x.Nombre);

            return View(vm);
        }
        
        public IActionResult Eliminar(int id)
        {
            var hamburguesa = RepoMenu.Get(id);

            if(hamburguesa == null)
            {
                return RedirectToAction("Index");
            }

            return View(hamburguesa);
        }

        [HttpPost]
        public IActionResult Eliminar(Menu m)
        {
            var hamburguesa = RepoMenu.Get(m.Id);
            if (hamburguesa == null)
            {
                return RedirectToAction("Index");
            }

            RepoMenu.Delete(hamburguesa);

            var ruta = $"wwwroot/hamburguesas/{m.Id}.jpg";

            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }

            return RedirectToAction("Index");
        }
    }
}
