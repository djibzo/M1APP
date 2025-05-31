using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1APP.Models
{
    public class AgenceReportViewModel
    {
   
        public string AdresseAgence { get; set; }

  
        public float Longitude { get; set; }


        public float Latitude { get; set; }

       
        public string NineaGestionnaire { get; set; }

    }

}