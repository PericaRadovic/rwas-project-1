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
    public class KlijentController : Controller
    {
        private ParketarskaRadnjaBaza db = new ParketarskaRadnjaBaza();

        // GET: Klijent
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Index()
        {
            return View(db.Klijenti.ToList());
        }

        // GET: Klijent/Details/5
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // GET: Klijent/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klijent/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ime,Prezime,Adresa,BrojTelefona,Email")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                db.Klijenti.Add(klijent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klijent);
        }

        // GET: Klijent/Edit/id
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijent/Edit/id
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlijentID,Ime,Prezime,Adresa,BrojTelefona,Email")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klijent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klijent);
        }

        // GET: Klijent/Delete/id
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = db.Klijenti.Find(id);
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijent/Delete/id
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klijent klijent = db.Klijenti.Find(id);
            db.Klijenti.Remove(klijent);
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