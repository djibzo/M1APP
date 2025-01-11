using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1APP.Models
{
    public class Offre
    {

        [Key]
        public int IdOffre { get; set; }
        [Display(Name = "Description"), Required(ErrorMessage = "*"), MaxLength(100)]
        public string DescriptionOffre { get; set; }
        [Display(Name = "Prix"), Required(ErrorMessage = "*")]
        public float PrixOffre { get; set; }
        [Display(Name = "Disponibilite"), Required(ErrorMessage = "*")]
        public bool DisponibiliteOffre { get; set; }
        public int idU { get; set; }  // Cet identifiant est une clé étrangère vers l'utilisateur

        // Cela définit la relation entre l'offre et l'utilisateur
        [ForeignKey("idU")]
        public virtual Utilisateur Utilisateur { get; set; }
    }
}