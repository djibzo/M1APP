using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using M1APP.Models;
using Microsoft.Owin.BuilderProperties;
using PagedList;

namespace M1APP.Controllers
{
    public class GestionnairesController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        const int pageSize = 10;

        // GET: Gestionnaires
        //string cni, int? page
        public ActionResult Index(string cni, int? page)
        {
            // Initialiser les ViewBag pour conserver les valeurs des filtres dans la vue
            ViewBag.CNI = !string.IsNullOrEmpty(cni) ? cni : string.Empty;

            // Récupérer tous les gestionnaires de la base de données
            var gestionnaires = db.gestionnaires.AsQueryable();

            // Appliquer les filtres
            if (!string.IsNullOrEmpty(cni))
            {
                gestionnaires = gestionnaires.Where(g => g.CNIGestionnaire.ToLower().Contains(cni.ToLower()));
            }

            // Initialiser la pagination
            int pageNumber = (page ?? 1);

            // Retourner la vue avec les résultats paginés
            return View(gestionnaires.OrderBy(g => g.CNIGestionnaire).ToPagedList(pageNumber, pageSize));
        }

        // GET: Gestionnaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            if (gestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gestionnaires/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur,CNIGestionnaire")] Gestionnaire gestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(gestionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gestionnaire);
        }

        // GET: Gestionnaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            if (gestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(gestionnaire);
        }

        // POST: Gestionnaires/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur,CNIGestionnaire")] Gestionnaire gestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            if (gestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(gestionnaire);
        }

        // POST: Gestionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            db.utilisateurs.Remove(gestionnaire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
