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
    public class ProizvodController : Controller
    {
        
        private ParketarskaRadnjaBaza db = new ParketarskaRadnjaBaza();
        public ActionResult Index()
        {
            return View(db.Proizvodi.ToList());
        }
        
        // GET: Proizvod/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }
        // GET: Proizvod/Create
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proizvod/Create
        [HttpPost]
        [Authorize(Roles = "Admin, Korisnik")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProizvodID,NazivProizvoda,Opis,Cena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Proizvodi.Add(proizvod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvod);
        }

        // GET: Proizvod/Edit/id
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvod/Edit/id
        [HttpPost]
        [Authorize(Roles = "Admin, Korisnik")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProizvodID,NazivProizvoda,Opis,Cena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvod);
        }

        // GET: Proizvod/Delete/id
        [Authorize(Roles = "Admin, Korisnik")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvod/Delete/id
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Korisnik")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvod proizvod = db.Proizvodi.Find(id);
            db.Proizvodi.Remove(proizvod);
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