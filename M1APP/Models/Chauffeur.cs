﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace M1APP.Models
{
    public class Chauffeur
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(80)]
        public string Nom { get; set; }
        [Required,MaxLength(80)]
        public string Prenom { get; set; }
    }
}