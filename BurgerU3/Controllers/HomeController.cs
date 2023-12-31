﻿using BurgerU3.Models.Entities;
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

        public IActionResult VerPromociones(int id)
        {
            PromocionesViewModel vm = new();

            vm.Index = id;
            var lista = MenuRepo.GetConPromociones();
            vm.Tamano = lista.Count();

            var hamburguesa = lista.ToList()[vm.Index];

            if(hamburguesa != null)
            {
                vm.menu = hamburguesa;
            }

            return View(vm);
        }
 
    }
}
