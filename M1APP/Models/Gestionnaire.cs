using System.ComponentModel.DataAnnotations;
namespace M1APP.Models
{
    public class Gestionnaire:Utilisateur
    {
        [Display(Name = "Ninea"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string NineaGestionnaire { get; set; }

        [Display(Name = "RCCM"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string RccmGestionnaire { get; set; }

        [Display(Name = "   CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }
    }
}