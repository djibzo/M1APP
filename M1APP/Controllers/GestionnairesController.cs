using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using M1APP.Models;

namespace M1APP.Controllers
{
    public class GestionnairesController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();

        // GET: Gestionnaires
        public ActionResult Index()
        {
            return View(db.gestionnaires.ToList());
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
