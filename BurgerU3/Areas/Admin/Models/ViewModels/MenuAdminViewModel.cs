using Microsoft.Build.Evaluation;

namespace BurgerU3.Areas.Admin.Models.ViewModels
{
    public class MenuAdminViewModel
    {
        public IEnumerable<ClasificacionModel> ListaClasificaciones { get; set; } = null!;
        public IEnumerable<BurgerModel> ListaH { get; set; } = null!;   
    }

    public class ClasificacionModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class BurgerModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Clasificacion { get; set; } = null!;
    }
}
