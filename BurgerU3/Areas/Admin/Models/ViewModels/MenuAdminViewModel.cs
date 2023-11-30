using BurgerU3.Models.Entities;
using Microsoft.Build.Evaluation;

namespace BurgerU3.Areas.Admin.Models.ViewModels
{
    public class MenuAdminViewModel
    {
        
        public IEnumerable<Clasificacion> ListaClasificaciones { get; set; } = null!;
    }

    
}
