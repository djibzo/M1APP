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
        const int pageSize= 1;

        // GET: Agences
        public ActionResult Index(string Adresse,string ninea,string rccm,int? page)
        {
            // TODO viewbag ?
            ViewBag.Adresse = Adresse!=null? Adresse : string.Empty;
            ViewBag.ninea = ninea!=null? ninea : string.Empty;
            ViewBag.rccm = rccm!=null? rccm : string.Empty;

            var agences = db.agences.Include(a => a.Gestionnaire);
            var liste = agences.ToList();
            if (!string.IsNullOrEmpty(Adresse))
            {
                liste = liste.Where(a => Adresse.ToLower().Contains(Adresse.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(ninea))
            {
                liste = liste.Where(a => ninea.ToLower().Contains(ninea.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(rccm))
            {
                liste = liste.Where(a => rccm.ToLower().Contains(rccm.ToLower())).ToList();
            }
            //initialiser page 
            page = page.HasValue ? page.Value : 1;
            int pageNumber = (int)page;
            return View(liste.ToPagedList(pageNumber, pageSize));
            //return View(agences.ToList());
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
