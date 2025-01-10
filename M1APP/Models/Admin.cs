using System.ComponentModel.DataAnnotations;
namespace M1APP.Models
{
    public class Admin:Utilisateur
    {
        [Display(Name = "Matricule"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string MatriculeAdmin { get; set; }
    }
}