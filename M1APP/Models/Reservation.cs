using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1APP.Models
{
    public class Reservation
    {
        [Key]
        public int IdRéservation { get; set; }
        [Display(Name = "Date"), Required(ErrorMessage = "*")]
        public DateTime DateRéservation { get; set; }
        [Display(Name = "Montant"), Required(ErrorMessage = "*")]
        public float MontantRéservation { get; set; }
        [Display(Name = "Statut"), Required(ErrorMessage = "*")]
        public string StatutRéservation { get; set; }
        [ForeignKey("idU")]
        public virtual Utilisateur Utilisateur { get; set; }
    }
}