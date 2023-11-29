using BurgerU3.Models.Entities;
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
