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
    public class SztabController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Sztab
        public ActionResult Index()
        {
            var sztabs = db.Sztabs.Include(s => s.Funckje);
            ViewBag.Id_Funkcji = new SelectList(db.Funckjes, "Id_Funkcji", "Funkcja");
            return View(sztabs.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Search, int Id_Funkcji)
        {
            var sztabs = db.Sztabs.Include(s => s.Funckje).Where(a => a.Id_Funkcji == Id_Funkcji);
            ViewBag.Id_Funkcji = new SelectList(db.Funckjes, "Id_Funkcji", "Funkcja");

            if (!String.IsNullOrEmpty(Search))
            {
                sztabs = sztabs.Where(s => s.Nazwisko.Contains(Search) || s.Imię.Contains(Search));
            }

            return View(sztabs.ToList());
        }

        // GET: Sztab/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sztab sztab = db.Sztabs.Find(id);
            if (sztab == null)
            {
                return HttpNotFound();
            }
            return View(sztab);
        }

        // GET: Sztab/Create
        public ActionResult Create()
        {
            ViewBag.Id_Funkcji = new SelectList(db.Funckjes, "Id_Funkcji", "Funkcja");
            return View();
        }

        // POST: Sztab/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imię,Nazwisko,Id_Funkcji,Zdjęcie")] Sztab sztab, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid && ImageFile != null)
            {
                string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                ImageFile.SaveAs(physicalPath);
                sztab.Zdjęcie = "images/" + ImageName;

                db.Sztabs.Add(sztab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Funkcji = new SelectList(db.Funckjes, "Id_Funkcji", "Funkcja", sztab.Id_Funkcji);
            return View(sztab);
        }

        // GET: Sztab/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sztab sztab = db.Sztabs.Find(id);
            if (sztab == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Funkcji = new SelectList(db.Funckjes, "Id_Funkcji", "Funkcja", sztab.Id_Funkcji);
            return View(sztab);
        }

        // POST: Sztab/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Imię,Nazwisko,Id_Funkcji,Zdjęcie")] Sztab sztab, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                ImageFile.SaveAs(physicalPath);
                sztab.Zdjęcie = "images/" + ImageName;

                db.Entry(sztab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Funkcji = new SelectList(db.Funckjes, "Id_Funkcji", "Funkcja", sztab.Id_Funkcji);
            return View(sztab);
        }

        // GET: Sztab/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sztab sztab = db.Sztabs.Find(id);
            if (sztab == null)
            {
                return HttpNotFound();
            }
            return View(sztab);
        }

        // POST: Sztab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sztab sztab = db.Sztabs.Find(id);
            db.Sztabs.Remove(sztab);
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
