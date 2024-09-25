using ParketarskaRadnja.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ParketarskaRadnja.Controllers
{
    public class NarudzbineController : Controller
    {

        private ParketarskaRadnjaBaza db = new ParketarskaRadnjaBaza();

        // GET: Narudzbine
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Index()
        {
            var narudzbine = db.Narudzbine.Include(n => n.Klijent);
            return View(narudzbine.ToList());
        }

        // GET: Narudzbine/Details/5
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbine narudzbine = db.Narudzbine.Include(n=>n.Klijent).FirstOrDefault(n=>n.NarudzbinaID==id);
            
            if (narudzbine == null)
            {
                return HttpNotFound();
            }
            return View(narudzbine);
        }

        // GET: Narudzbine/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.KlijentiList = new SelectList(db.Klijenti, "KlijentID", "Ime","Prezime");
            return View();
        }

        // POST: Narudzbine/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NarudzbinaID,KlijentID,DatumNarudzbine")] Narudzbine narudzbine)
        {
            if (ModelState.IsValid)
            {
                db.Narudzbine.Add(narudzbine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", narudzbine.KlijentID);
            return View(narudzbine);
        }

        // GET: Narudzbine/Edit/id
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbine narudzbine = db.Narudzbine.Find(id);
            
            if (narudzbine == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", narudzbine.KlijentID);
            return View(narudzbine);

        }

        // POST: Narudzbine/Edit/id
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NarudzbinaID,KlijentID,DatumNarudzbine")] Narudzbine narudzbine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(narudzbine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlijentID = new SelectList(db.Klijenti, "KlijentID", "Ime", narudzbine.KlijentID);
            return View(narudzbine);

        }

        // GET: Narudzbine/Delete/id
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbine narudzbine = db.Narudzbine.Include(n => n.Klijent).FirstOrDefault(n => n.NarudzbinaID == id);
            if (narudzbine == null)
            {
                return HttpNotFound();
            }
            return View(narudzbine);
        }

        // POST: Narudzbine/Delete/id
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narudzbine narudzbine = db.Narudzbine.Find(id);
            db.Narudzbine.Remove(narudzbine);
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