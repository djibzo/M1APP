using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M1APP.Models;

namespace M1APP.Controllers
{
    public class ClientAjaxController : Controller
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
            var clients = db.Clients.ToList();
            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        // POST: ClientAjax/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "IdUtilisateur,CniClient,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return Json(new { success = true, message = "Client créé avec succès." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Erreur lors de la création du client.", errors = errors });
        }




        // GET: ClientAjax/Details/5
        public JsonResult Details(int id)
        {
            var client = db.Clients.Find(id);
            if (client == null)
            {
                return Json(new { success = false, message = "Client non trouvé." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, data = client }, JsonRequestBehavior.AllowGet);
        }

        // POST: ClientAjax/Edit/5
        [HttpPost]
        public JsonResult Edit(Client client)
        {
            // Validation manuelle des données
            if (string.IsNullOrEmpty(client.CniClient) ||
                string.IsNullOrEmpty(client.NomUtilisateur) ||
                string.IsNullOrEmpty(client.PrenomUtilisateur) ||
                string.IsNullOrEmpty(client.EmailUtilisateur) ||
                string.IsNullOrEmpty(client.TelUtilisateur))
            {
                return Json(new { success = false, message = "Tous les champs sont obligatoires." });
            }

            // Mise à jour du client dans la base de données
            db.Entry(client).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true, message = "Client mis à jour avec succès." });
        }


        // POST: ClientAjax/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var client = db.Clients.Find(id);
            if (client == null)
            {
                return Json(new { success = false, message = "Client non trouvé." });
            }
            db.Clients.Remove(client);
            db.SaveChanges();
            return Json(new { success = true, message = "Client supprimé avec succès." });
        }


        public JsonResult Search(string query)
        {
            var clients = db.Clients
                .Where(c => c.CniClient.Contains(query) ||
                            c.NomUtilisateur.Contains(query) ||
                            c.PrenomUtilisateur.Contains(query) ||
                            c.EmailUtilisateur.Contains(query) ||
                            c.TelUtilisateur.Contains(query))
                .ToList();
            return Json(clients, JsonRequestBehavior.AllowGet);
        }

    }
}
