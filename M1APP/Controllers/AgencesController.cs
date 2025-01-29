using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using M1APP.Models;
using PagedList;
namespace M1APP.Controllers
{
    public class AgencesController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        const int pageSize= 10;

        // GET: Agences
        public ActionResult Index(string adresse, string ninea, string rccm, int? page)
        {
            // Initialiser les ViewBag pour conserver les valeurs des filtres dans la vue
            ViewBag.Adresse = !string.IsNullOrEmpty(adresse) ? adresse : string.Empty;
            ViewBag.Ninea = !string.IsNullOrEmpty(ninea) ? ninea : string.Empty;
            ViewBag.Rccm = !string.IsNullOrEmpty(rccm) ? rccm : string.Empty;

            // Récupérer toutes les agences de la base de données
            var agences = db.agences.AsQueryable();

            // Appliquer les filtres
            if (!string.IsNullOrEmpty(adresse))
            {
                agences = agences.Where(a => a.AdresseAgence.ToLower().Contains(adresse.ToLower()));
            }
            if (!string.IsNullOrEmpty(ninea))
            {
                agences = agences.Where(a => a.NineaGestionnaire.ToLower().Contains(ninea.ToLower()));
            }
            if (!string.IsNullOrEmpty(rccm))
            {
                agences = agences.Where(a => a.RccmGestionnaire.ToLower().Contains(rccm.ToLower()));
            }

            // Initialiser la pagination
            int pageNumber = (page ?? 1);

            // Retourner la vue avec les résultats paginés
            return View(agences.OrderBy(a => a.AdresseAgence).ToPagedList(pageNumber, pageSize));
        }

        // GET: Agences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // GET: Agences/Create
        public ActionResult Create()
        {
            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: Agences/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAgence,AdresseAgence,Longitude,Latitude,NineaGestionnaire,RccmGestionnaire,IdGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.agences.Add(agence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "NomUtilisateur", agence.IdGestionnaire);
            return View(agence);
        }

        // GET: Agences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "NomUtilisateur", agence.IdGestionnaire);
            return View(agence);
        }

        // POST: Agences/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAgence,AdresseAgence,Longitude,Latitude,NineaGestionnaire,RccmGestionnaire,IdGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "NomUtilisateur", agence.IdGestionnaire);
            return View(agence);
        }

        // GET: Agences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agence agence = db.agences.Find(id);
            db.agences.Remove(agence);
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
