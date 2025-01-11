using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace M1APP.Models
{
    public class Agence
    {
        [Key]
        public int IdAgence { get; set; }
        public string AdresseAgence { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        [Display(Name = "Ninea"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string NineaGestionnaire { get; set; }

        [Display(Name = "RCCM"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string RccmGestionnaire { get; set; }
    }
}