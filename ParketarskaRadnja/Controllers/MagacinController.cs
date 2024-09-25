using ParketarskaRadnja.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ParketarskaRadnja.Controllers
{
    public class MagacinController : Controller
    {
        private ParketarskaRadnjaBaza db = new ParketarskaRadnjaBaza();

        // GET: Magacin
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var magacin = db.Magacini.Include(m => m.Proizvod);

            if (!String.IsNullOrEmpty(searchString))
            {
                magacin = magacin.Where(m => m.Proizvod.NazivProizvoda.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    magacin = magacin.OrderByDescending(m => m.Proizvod.NazivProizvoda);
                    break;
                case "Quantity":
                    magacin = magacin.OrderBy(m => m.KolicinaNaZalihi);
                    break;
                case "quantity_desc":
                    magacin = magacin.OrderByDescending(m => m.KolicinaNaZalihi);
                    break;
                default:
                    magacin = magacin.OrderBy(m => m.Proizvod.NazivProizvoda);
                    break;
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(magacin.ToPagedList(pageNumber, pageSize));
        }

        // GET: Magacin/Details/id
        [Authorize(Roles = "Admin,Korisnik")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magacin magacin = db.Magacini.Include(m => m.Proizvod).FirstOrDefault(m => m.StanjeID == id);
            if (magacin == null)
            {
                return HttpNotFound();
            }
            return View(magacin);
        }

        // GET: Magacin/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda");
            return View();
        }

        // POST: Magacin/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StanjeID,ProizvodID,KolicinaNaZalihi")] Magacin magacin)
        {
            if (ModelState.IsValid)
            {
                db.Magacini.Add(magacin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda", magacin.ProizvodID);
            return View(magacin);
        }

        // GET: Magacin/Edit/id
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magacin magacin = db.Magacini.Find(id);
            if (magacin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda", magacin.ProizvodID);
            return View(magacin);
        }

        // POST: Magacin/Edit/id
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StanjeID,ProizvodID,KolicinaNaZalihi")] Magacin magacin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magacin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProizvodID = new SelectList(db.Proizvodi, "ProizvodID", "NazivProizvoda", magacin.ProizvodID);
            return View(magacin);
        }

        // GET: Magacin/Delete/id
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magacin magacin = db.Magacini.Include(m => m.Proizvod).FirstOrDefault(m => m.StanjeID == id);
            if (magacin == null)
            {
                return HttpNotFound();
            }
            return View(magacin);
        }

        // POST: Magacin/Delete/id
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magacin magacin = db.Magacini.Find(id);
            db.Magacini.Remove(magacin);
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