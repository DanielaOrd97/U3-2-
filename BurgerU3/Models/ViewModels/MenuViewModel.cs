using BurgerU3.Models.Entities;

namespace BurgerU3.Models.ViewModels
{
    public class MenuViewModel
    {
        public int IdSeleccionado { get; set; }
        public string Descripcion { get; set; } = null!;
        public IEnumerable<Clasificacion> ListaClasificaciones { get; set; } = null!;

    }


    
}

