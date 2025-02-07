using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M1APP.Models;

namespace M1APP.Controllers
{
    public class AdminAjaxController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();

        // GET: ClientAjax
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClientAjax/List
        public JsonResult List()
        {
            var admin = db.Admins.ToList();
            return Json(admin, JsonRequestBehavior.AllowGet);
        }

        // POST: ClientAjax/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur,MatriculeAdmin")] Admin admin)
        {
            /* if (ModelState.IsValid)
             {
                 db.Admins.Add(admin);
                 db.SaveChanges();
                 return Json(new { success = true, message = "Admin créé avec succès." });
             }
             else
             {

                 var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                 return Json(new { success = false, message = "Erreur lors de la création de l'admin.", errors = errors });
             }*/
            try
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return Json(new { success = true, message = "Admin créé avec succès." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"erreur : {ex.Message.ToString()}" });

            }

        }




        // GET: ClientAjax/Details/5
        public JsonResult Details(int id)
        {
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return Json(new { success = false, message = "admin non trouvé." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, data = admin }, JsonRequestBehavior.AllowGet);
        }

        // POST: ClientAjax/Edit/5
        [HttpPost]
        public JsonResult Edit(Admin admin)
        {
            // Validation manuelle des données
            if (string.IsNullOrEmpty(admin.MatriculeAdmin) ||
                string.IsNullOrEmpty(admin.NomUtilisateur) ||
                string.IsNullOrEmpty(admin.PrenomUtilisateur) ||
                string.IsNullOrEmpty(admin.EmailUtilisateur) ||
                string.IsNullOrEmpty(admin.TelUtilisateur))
            {
                return Json(new { success = false, message = "Tous les champs sont obligatoires." });
            }

            // Mise à jour du client dans la base de données
            db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true, message = "Client mis à jour avec succès." });
        }


        // POST: ClientAjax/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return Json(new { success = false, message = "Admin non trouvé." });
            }
            db.Admins.Remove(admin);
            db.SaveChanges();
            return Json(new { success = true, message = "Admin supprimé avec succès." });
        }


        public JsonResult Search(string query)
        {
            var admins = db.Admins
                .Where(c => c.MatriculeAdmin.Contains(query) ||
                            c.NomUtilisateur.Contains(query) ||
                            c.PrenomUtilisateur.Contains(query) ||
                            c.EmailUtilisateur.Contains(query) ||
                            c.TelUtilisateur.Contains(query))
                .ToList();
            return Json(admins, JsonRequestBehavior.AllowGet);
        }

    }
}
