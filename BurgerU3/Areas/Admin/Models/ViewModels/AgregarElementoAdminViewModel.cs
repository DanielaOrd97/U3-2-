using BurgerU3.Models.Entities;

namespace BurgerU3.Areas.Admin.Models.ViewModels
{
    public class AgregarElementoAdminViewModel
    {
        //public Menu menu { get; set; } = new();
        //public IEnumerable<ClasificacionModel> ListaClasificaciones { get; set; } = null!;
        //public IFormFile? Archivo { get; set; }

        public Menu menu { get; set; } = new();
        public IEnumerable<Clasificacion> ListaClasificaciones { get; set; } = null!;
        public IFormFile? Archivo { get; set; }
    }
}
