﻿using BurgerU3.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BurgerU3.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
        }

        public override IEnumerable<Menu> GetAll()
        {
            return Context.Menu
                .Include(x => x.IdClasificacionNavigation)
                .OrderBy(x => x.Nombre);
        }

        public  Menu? GetByNombre(string nombre)
        {
            return Context.Menu.Include(x => x.IdClasificacionNavigation).FirstOrDefault(x => x.Nombre == nombre);
        }
       

        public List<Menu> GetConPromociones()
        {
            return Context.Menu
                .Where(x => x.PrecioPromocion > 0).ToList();

        }
    }

    public class ClasifRepository : Repository<Clasificacion>
    {
        public ClasifRepository(NeatContext context) : base(context)
        {
        }

        public override IEnumerable<Clasificacion> GetAll()
        {
            return Context.Clasificacion
                .Include(x => x.Menu);
        }
    }
}
