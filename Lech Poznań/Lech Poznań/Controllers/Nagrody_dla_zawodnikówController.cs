using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lech_Poznań.Models;

namespace Lech_Poznań.Controllers
{
    public class Nagrody_dla_zawodnikówController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Nagrody_dla_zawodników
        public ActionResult Index()
        {
            var nagrody_dla_zawodników = db.Nagrody_dla_zawodników.Include(n => n.Nagrody).Include(n => n.Zawodnicy);
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda");
            return View(nagrody_dla_zawodników.ToList());
        }

        [HttpPost]
        public ActionResult Index(int Id_Nagrody)
        {
            var nagrody_dla_zawodników = db.Nagrody_dla_zawodników.Include(n => n.Nagrody).Include(n => n.Zawodnicy).Where(a => a.Id_Nagrody == Id_Nagrody);
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda");
            return View(nagrody_dla_zawodników.ToList());
        }

        // GET: Nagrody_dla_zawodników/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody_dla_zawodników nagrody_dla_zawodników = db.Nagrody_dla_zawodników.Find(id);
            if (nagrody_dla_zawodników == null)
            {
                return HttpNotFound();
            }
            return View(nagrody_dla_zawodników);
        }

        // GET: Nagrody_dla_zawodników/Create
        public ActionResult Create()
        {
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda");
            ViewBag.Id_Zawodnika = new SelectList(db.Zawodnicies, "Id_Zawodnika", "FullName");
            return View();
        }

        // POST: Nagrody_dla_zawodników/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNZ,Id_Zawodnika,Id_Nagrody")] Nagrody_dla_zawodników nagrody_dla_zawodników)
        {
            if (ModelState.IsValid)
            {
                db.Nagrody_dla_zawodników.Add(nagrody_dla_zawodników);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda", nagrody_dla_zawodników.Id_Nagrody);
            ViewBag.Id_Zawodnika = new SelectList(db.Zawodnicies, "Id_Zawodnika", "FullName", nagrody_dla_zawodników.Id_Zawodnika);
            return View(nagrody_dla_zawodników);
        }

        // GET: Nagrody_dla_zawodników/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody_dla_zawodników nagrody_dla_zawodników = db.Nagrody_dla_zawodników.Find(id);
            if (nagrody_dla_zawodników == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda", nagrody_dla_zawodników.Id_Nagrody);
            ViewBag.Id_Zawodnika = new SelectList(db.Zawodnicies, "Id_Zawodnika", "FullName", nagrody_dla_zawodników.Id_Zawodnika);
            return View(nagrody_dla_zawodników);
        }

        // POST: Nagrody_dla_zawodników/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNZ,Id_Zawodnika,Id_Nagrody")] Nagrody_dla_zawodników nagrody_dla_zawodników)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nagrody_dla_zawodników).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda", nagrody_dla_zawodników.Id_Nagrody);
            ViewBag.Id_Zawodnika = new SelectList(db.Zawodnicies, "Id_Zawodnika", "Imię", nagrody_dla_zawodników.Id_Zawodnika);
            return View(nagrody_dla_zawodników);
        }

        // GET: Nagrody_dla_zawodników/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody_dla_zawodników nagrody_dla_zawodników = db.Nagrody_dla_zawodników.Find(id);
            if (nagrody_dla_zawodników == null)
            {
                return HttpNotFound();
            }
            return View(nagrody_dla_zawodników);
        }

        // POST: Nagrody_dla_zawodników/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nagrody_dla_zawodników nagrody_dla_zawodników = db.Nagrody_dla_zawodników.Find(id);
            db.Nagrody_dla_zawodników.Remove(nagrody_dla_zawodników);
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
