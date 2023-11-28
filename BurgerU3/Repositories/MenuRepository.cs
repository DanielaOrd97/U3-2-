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
}
