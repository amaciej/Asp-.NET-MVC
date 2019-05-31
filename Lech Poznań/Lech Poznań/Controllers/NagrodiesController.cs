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
    public class NagrodiesController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Nagrodies
        public ActionResult Index()
        {
            return View(db.Nagrodies.ToList());
        }

        // GET: Nagrodies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody nagrody = db.Nagrodies.Find(id);
            if (nagrody == null)
            {
                return HttpNotFound();
            }
            return View(nagrody);
        }

        // GET: Nagrodies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nagrodies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Nagrody,Nagroda")] Nagrody nagrody)
        {
            if (ModelState.IsValid)
            {
                db.Nagrodies.Add(nagrody);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nagrody);
        }

        // GET: Nagrodies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody nagrody = db.Nagrodies.Find(id);
            if (nagrody == null)
            {
                return HttpNotFound();
            }
            return View(nagrody);
        }

        // POST: Nagrodies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Nagrody,Nagroda")] Nagrody nagrody)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nagrody).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nagrody);
        }

        // GET: Nagrodies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody nagrody = db.Nagrodies.Find(id);
            if (nagrody == null)
            {
                return HttpNotFound();
            }
            return View(nagrody);
        }

        // POST: Nagrodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nagrody nagrody = db.Nagrodies.Find(id);
            db.Nagrodies.Remove(nagrody);
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
