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
    public class PozycjeController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Pozycje
        public ActionResult Index()
        {
            return View(db.Pozycjes.ToList());
        }

        // GET: Pozycje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycje pozycje = db.Pozycjes.Find(id);
            if (pozycje == null)
            {
                return HttpNotFound();
            }
            return View(pozycje);
        }

        // GET: Pozycje/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pozycje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pozycji,Pozycja")] Pozycje pozycje)
        {
            if (ModelState.IsValid)
            {
                db.Pozycjes.Add(pozycje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pozycje);
        }

        // GET: Pozycje/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycje pozycje = db.Pozycjes.Find(id);
            if (pozycje == null)
            {
                return HttpNotFound();
            }
            return View(pozycje);
        }

        // POST: Pozycje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pozycji,Pozycja")] Pozycje pozycje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pozycje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pozycje);
        }

        // GET: Pozycje/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pozycje pozycje = db.Pozycjes.Find(id);
            if (pozycje == null)
            {
                return HttpNotFound();
            }
            return View(pozycje);
        }

        // POST: Pozycje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pozycje pozycje = db.Pozycjes.Find(id);
            db.Pozycjes.Remove(pozycje);
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
