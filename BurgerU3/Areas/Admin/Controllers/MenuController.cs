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
            AgregarElementoAdminViewModel vm = new();
            vm.ListaClasificaciones = RepositorioClasif.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            }).ToList();

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
                //if (vm.Archivo.ContentType != "image/jepg")
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
                RepositorioM.Insert(vm.menu);

                if(vm.Archivo == null)
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

            vm.ListaClasificaciones = RepositorioClasif.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionModel
            {
                Nombre = x.Nombre,
            }).ToList();

            return View(vm);
        }


        public IActionResult Editar(int id)
        {
            var elemento = RepositorioM.Get(id);

            if(elemento == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AgregarElementoAdminViewModel vm = new();
                vm.menu = elemento;
                vm.ListaClasificaciones = RepositorioClasif.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                }).ToList();

                return View(vm);
            }
            
        }

        [HttpPost]
        public IActionResult Editar(AgregarElementoAdminViewModel vm)
        {
            ModelState.Clear();
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/jepg")
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
                var elemento = RepositorioM.Get(vm.menu.Id);

                if(elemento == null)
                {
                    return RedirectToAction("Index");
                }

                elemento.Nombre = vm.menu.Nombre;
                elemento.Precio = vm.menu.Precio;
                elemento.Descripción = vm.menu.Descripción;
                elemento.IdClasificacion = vm.menu.IdClasificacion;


                RepositorioM.Update(elemento);

                
                //Copia la nueva ruta.
                if(vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.menu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }

            vm.ListaClasificaciones = RepositorioClasif.GetAll().OrderBy(x => x.Nombre).Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            }).ToList();

            return View(vm);
        }

        public IActionResult Eliminar(int id)
        {
            var elemento = RepositorioM.Get(id);

            if (elemento == null)
            {
                return RedirectToAction("Index");
            }
            return View(elemento);
        }

        [HttpPost]
        public IActionResult Eliminar(Menu m)
        {
            var elemento = RepositorioM.Get(m.Id);
            if(elemento == null)
            {
                return RedirectToAction("Index");
            }

            RepositorioM.Delete(elemento);

            var ruta = $"wwwroot/hamburguesas/{m.Id}.jpg";

            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }

            return RedirectToAction("Index");
        }

    }
}
