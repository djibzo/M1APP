using System.ComponentModel.DataAnnotations;
namespace M1APP.Models
{
    public class Client:Utilisateur
    {
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CniClient { get; set; }
    }
}