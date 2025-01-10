using System.ComponentModel.DataAnnotations;
namespace M1APP.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }
        [Display(Name ="Nom"),Required(ErrorMessage ="*"),MaxLength(80)]
        public string NomUtilisateur { get; set; }
        [Display(Name = "Prenom"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string PrenomUtilisateur { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string EmailUtilisateur { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password"), Required(ErrorMessage = "*"), MaxLength(255)]
        public string PasswordUtilisateur { get; set; }

        [Display(Name = "Telephone"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string TelUtilisateur { get; set; }
    }
}