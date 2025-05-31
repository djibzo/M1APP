using System.ComponentModel.DataAnnotations;

namespace APITrip.Entities
{
    public class Gestionnaire : User
    {
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }

        /* public int IdAnnonce { get; set; }

         [ForeignKey("IdAnnonce")]
         public virtual Annonce Annonce { get; set; }
         */
        public virtual ICollection<Agence> Agences { get; set; }
    }
}
