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
    public class FunckjeController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Funckje
        public ActionResult Index()
        {
            return View(db.Funckjes.ToList());
        }

        // GET: Funckje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funckje funckje = db.Funckjes.Find(id);
            if (funckje == null)
            {
                return HttpNotFound();
            }
            return View(funckje);
        }

        // GET: Funckje/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funckje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Funkcji,Funkcja")] Funckje funckje)
        {
            if (ModelState.IsValid)
            {
                db.Funckjes.Add(funckje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funckje);
        }

        // GET: Funckje/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funckje funckje = db.Funckjes.Find(id);
            if (funckje == null)
            {
                return HttpNotFound();
            }
            return View(funckje);
        }

        // POST: Funckje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Funkcji,Funkcja")] Funckje funckje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funckje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funckje);
        }

        // GET: Funckje/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funckje funckje = db.Funckjes.Find(id);
            if (funckje == null)
            {
                return HttpNotFound();
            }
            return View(funckje);
        }

        // POST: Funckje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funckje funckje = db.Funckjes.Find(id);
            db.Funckjes.Remove(funckje);
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
