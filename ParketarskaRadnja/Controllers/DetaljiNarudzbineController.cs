using ParketarskaRadnja.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ParketarskaRadnja.Controllers
{
    public class DetaljiNarudzbineController : Controller
    {
        private ParketarskaRadnjaBaza db = new ParketarskaRadnjaBaza();
        // GET: DetaljiNarudzbine
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Index()
        {
            var detaljiNarudzbine = db.DetaljiNarudzbine.Include("Narudzbine").Include("Proizvod");
            return View(detaljiNarudzbine.ToList());
        }

        // GET: DetaljiNarudzbine/Details/5
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetaljiNarudzbine detaljiNarudzbine = db.DetaljiNarudzbine.Find(id);
            if (detaljiNarudzbine == null)
            {
                return HttpNotFound();
            }
            return View(detaljiNarudzbine);
        }

        // GET: DetaljiNarudzbine/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.NarudzbinaID = new SelectList(db.Narudzbine, "NarudzbinaID", "NarudzbinaID");
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda");
            return View();
        }

        // POST: DetaljiNarudzbine/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NarudzbinaID,ProizvodID,Kolicina,CenaPoJedinici")] DetaljiNarudzbine detaljiNarudzbine)
        {
            if (ModelState.IsValid)
            {
                db.DetaljiNarudzbine.Add(detaljiNarudzbine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NarudzbinaID = new SelectList(db.Narudzbine, "NarudzbinaID", "NarudzbinaID", detaljiNarudzbine.NarudzbinaID);
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda", detaljiNarudzbine.ProizvodID);
            return View(detaljiNarudzbine);
        }

        // GET: DetaljiNarudzbines/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetaljiNarudzbine detaljiNarudzbine = db.DetaljiNarudzbine.Find(id);
            if (detaljiNarudzbine == null)
            {
                return HttpNotFound();
            }
            ViewBag.NarudzbinaID = new SelectList(db.Narudzbine, "NarudzbinaID", "NarudzbinaID", detaljiNarudzbine.NarudzbinaID);
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda", detaljiNarudzbine.ProizvodID);
            return View(detaljiNarudzbine);
        }

        // POST: DetaljiNarudzbines/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DetaljNarudzbineID,NarudzbinaID,ProizvodID,Kolicina,CenaPoJedinici")] DetaljiNarudzbine detaljiNarudzbine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detaljiNarudzbine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NarudzbinaID = new SelectList(db.Narudzbine, "NarudzbinaID", "NarudzbinaID", detaljiNarudzbine.NarudzbinaID);
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda", detaljiNarudzbine.ProizvodID);
            return View(detaljiNarudzbine);
        }


        [Authorize(Roles = "Admin")]
        // GET: DetaljiNarudzbines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetaljiNarudzbine detaljiNarudzbine = db.DetaljiNarudzbine.Find(id);
            if (detaljiNarudzbine == null)
            {
                return HttpNotFound();
            }
            return View(detaljiNarudzbine);
        }

        // POST: DetaljiNarudzbines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetaljiNarudzbine detaljiNarudzbine = db.DetaljiNarudzbine.Find(id);
            db.DetaljiNarudzbine.Remove(detaljiNarudzbine);
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