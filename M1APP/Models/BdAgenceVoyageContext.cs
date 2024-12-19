using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace M1APP.Models
{
    public class BdAgenceVoyageContext:DbContext
    {
        public BdAgenceVoyageContext():base("connAgenceVoyage")
        { }
        public DbSet<Chauffeur> chauffeurs { get; set; }
    }
}