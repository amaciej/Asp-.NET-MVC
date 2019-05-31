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
    public class Nagrody_dla_sztabuController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Nagrody_dla_sztabu
        public ActionResult Index()
        {
            var nagrody_dla_sztabus = db.Nagrody_dla_sztabus.Include(n => n.Nagrody).Include(n => n.Sztab);
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda");
            return View(nagrody_dla_sztabus.ToList());
        }

        [HttpPost]
        public ActionResult Index(int Id_Nagrody)
        {
            var nagrody_dla_sztabus = db.Nagrody_dla_sztabus.Include(n => n.Nagrody).Include(n => n.Sztab).Where(a => a.Id_Nagrody == Id_Nagrody);
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda");
            return View(nagrody_dla_sztabus.ToList());
        }

        // GET: Nagrody_dla_sztabu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody_dla_sztabu nagrody_dla_sztabu = db.Nagrody_dla_sztabus.Find(id);
            if (nagrody_dla_sztabu == null)
            {
                return HttpNotFound();
            }
            return View(nagrody_dla_sztabu);
        }

        // GET: Nagrody_dla_sztabu/Create
        public ActionResult Create()
        {
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda");
            ViewBag.ID = new SelectList(db.Sztabs, "ID", "FullName");
            return View();
        }

        // POST: Nagrody_dla_sztabu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNSz,ID,Id_Nagrody")] Nagrody_dla_sztabu nagrody_dla_sztabu)
        {
            if (ModelState.IsValid)
            {
                db.Nagrody_dla_sztabus.Add(nagrody_dla_sztabu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda", nagrody_dla_sztabu.Id_Nagrody);
            ViewBag.ID = new SelectList(db.Sztabs, "ID", "FullName", nagrody_dla_sztabu.ID);
            return View(nagrody_dla_sztabu);
        }

        // GET: Nagrody_dla_sztabu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody_dla_sztabu nagrody_dla_sztabu = db.Nagrody_dla_sztabus.Find(id);
            if (nagrody_dla_sztabu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda", nagrody_dla_sztabu.Id_Nagrody);
            ViewBag.ID = new SelectList(db.Sztabs, "ID", "FullName", nagrody_dla_sztabu.ID);
            return View(nagrody_dla_sztabu);
        }

        // POST: Nagrody_dla_sztabu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNSz,ID,Id_Nagrody")] Nagrody_dla_sztabu nagrody_dla_sztabu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nagrody_dla_sztabu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Nagrody = new SelectList(db.Nagrodies, "Id_Nagrody", "Nagroda", nagrody_dla_sztabu.Id_Nagrody);
            ViewBag.ID = new SelectList(db.Sztabs, "ID", "Imię", nagrody_dla_sztabu.ID);
            return View(nagrody_dla_sztabu);
        }

        // GET: Nagrody_dla_sztabu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nagrody_dla_sztabu nagrody_dla_sztabu = db.Nagrody_dla_sztabus.Find(id);
            if (nagrody_dla_sztabu == null)
            {
                return HttpNotFound();
            }
            return View(nagrody_dla_sztabu);
        }

        // POST: Nagrody_dla_sztabu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nagrody_dla_sztabu nagrody_dla_sztabu = db.Nagrody_dla_sztabus.Find(id);
            db.Nagrody_dla_sztabus.Remove(nagrody_dla_sztabu);
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
