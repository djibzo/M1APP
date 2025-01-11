using System.ComponentModel.DataAnnotations;
namespace M1APP.Models
{
    public class Gestionnaire:Utilisateur
    {
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }
    }
}