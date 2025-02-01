using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using M1APP.Models;
using Microsoft.Owin.BuilderProperties;
using PagedList;

namespace M1APP.Controllers
{
    public class AdminsController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        const int pageSize = 5;

        // GET: Admins
        public ActionResult Index(string matricule, int? page)
        {
            // Initialiser les ViewBag pour conserver les valeurs des filtres dans la vue
            ViewBag.MatriculeAdmin = !string.IsNullOrEmpty(matricule) ? matricule : string.Empty;

            // Récupérer tous les administrateurs de la base de données
            // AsQueryable permet d'appliquer les filtres directement sur la requête sans avoir à convertir la liste en mémoire.
            var admins = db.Admins.AsQueryable();

            // Appliquer les filtres
            if (!string.IsNullOrEmpty(matricule))
            {
                admins = admins.Where(a => a.MatriculeAdmin.ToLower().Contains(matricule.ToLower()));
            }

            // Vérifier si la liste des admin est vide
            bool noResults = !admins.Any();
            ViewBag.NoResults = noResults;
            page = page.HasValue ? page.Value : 1;
            int pageNumber = (int)page;

            // Retourner la vue avec les résultats paginés
            return View(admins.OrderBy(a => a.MatriculeAdmin).ToPagedList(pageNumber, pageSize));
        }



        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,MatriculeAdmin,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,MatriculeAdmin,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
