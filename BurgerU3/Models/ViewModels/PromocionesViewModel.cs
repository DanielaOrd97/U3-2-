using BurgerU3.Models.Entities;

namespace BurgerU3.Models.ViewModels
{
    public class PromocionesViewModel
    {
        public Menu menu { get; set; } = null!;
        //public List<Menu> ListaPromos { get; set; } = null!;
        public int Index { get; set; }
        public int Tamano { get; set; }

    }
}
