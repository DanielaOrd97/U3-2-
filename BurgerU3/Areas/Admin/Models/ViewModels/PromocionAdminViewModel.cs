using BurgerU3.Models.Entities;

namespace BurgerU3.Areas.Admin.Models.ViewModels
{
    public class PromocionAdminViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
        public double? PrecioPromocion { get; set; }

        //public Menu menu { get; set; } = new Menu();
    }
}
