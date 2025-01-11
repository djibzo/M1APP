using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace M1APP.Models
{
    public class Flotte
    {
        [Key]
        public int IdFlotte { get; set; }
        [Display(Name = "Type"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string TypeFlotte { get; set; }
        [Display(Name = "Matricule"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string MatriculeFlotte { get; set; }
    }
}