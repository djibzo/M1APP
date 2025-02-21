using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using M1APP.Models;
using M1APP.utils;
using PagedList;
namespace M1APP.Controllers
{
    public class ClientsController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        const int pageSize = 5;
        // GET: Clients
        public ActionResult Index(string cni, int? page)
        {
            ViewBag.cni = cni != null ? cni : string.Empty;
            // Récupère tous les administrateurs de la base de données
            var clients = db.Clients.AsQueryable();
            var FilteredClients = clients;
            if (!string.IsNullOrEmpty(cni))
            {
                FilteredClients = clients.Where(a => a.CniClient.ToLower().Contains(cni.ToLower()));
            }
            // Vérifier si la liste des clients est vide
            bool noResults = !clients.Any();
            ViewBag.NoResults = noResults;
            page = page.HasValue ? page.Value : 1;
            int pageNumber = (int)page;
            return View(FilteredClients.OrderBy(a => a.CniClient).ToPagedList(pageNumber, pageSize));

            //return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "IdUtilisateur,CniClient,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur")] Client client)
        {
            GMailer gmailler = new GMailer();
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                gmailler.SendEmail(client.EmailUtilisateur, "Inscription avec success !", $"Bonjour {client.PrenomUtilisateur +" "+ client.NomUtilisateur} , votre inscription à été bien enregistrée");
                await TwilioWhatsAppClient.SendWhatsAppMessage("+221772133001", "Hello, ceci est un test via Twilio WhatsApp !");
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,CniClient,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,PasswordUtilisateur,TelUtilisateur")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
