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
    public class ZawodnicyController : Controller
    {
        private LechPoznanEntities db = new LechPoznanEntities();

        // GET: Zawodnicy
        public ActionResult Index()
        {
            var zawodnicies = db.Zawodnicies.Include(z => z.Pozycje);
            ViewBag.Id_Pozycji = new SelectList(db.Pozycjes, "Id_Pozycji", "Pozycja");
            
            return View(zawodnicies.ToList());
        }
        [HttpPost]
        public ActionResult Index(string Search, int Id_Pozycji)
        {
            var zawodnicies = db.Zawodnicies.Include(z => z.Pozycje);
            ViewBag.Id_Pozycji = new SelectList(db.Pozycjes, "Id_Pozycji", "Pozycja");            
            zawodnicies = db.Zawodnicies.Include(z => z.Pozycje).Where(a => a.Id_Pozycji == Id_Pozycji);

            if (!String.IsNullOrEmpty(Search))
            {
                zawodnicies = zawodnicies.Where(s => s.Nazwisko.Contains(Search) || s.Imię.Contains(Search));
            }

            return View(zawodnicies.ToList());
        }

        //[HttpPost]
        //public ActionResult Index(string Search)
        //{
        //    var zawodnicies = db.Zawodnicies.Include(z => z.Pozycje);

            

        //    return View(zawodnicies.ToList());
        //}



        // GET: Zawodnicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnicy zawodnicy = db.Zawodnicies.Find(id);
            if (zawodnicy == null)
            {
                return HttpNotFound();
            }
            return View(zawodnicy);
        }

        // GET: Zawodnicy/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pozycji = new SelectList(db.Pozycjes, "Id_Pozycji", "Pozycja");
            return View();
        }

        // POST: Zawodnicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Zawodnika,Imię,Nazwisko,Wiek,Id_Pozycji,Ilość_występów,Strzelone_gole,Asysty,Czyste_konta,Zdjęcie")] Zawodnicy zawodnicy, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid && ImageFile != null)
            {
                string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                ImageFile.SaveAs(physicalPath);
                zawodnicy.Zdjęcie = "images/" + ImageName;

               
                db.Zawodnicies.Add(zawodnicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pozycji = new SelectList(db.Pozycjes, "Id_Pozycji", "Pozycja", zawodnicy.Id_Pozycji);
            return View(zawodnicy);
        }

        // GET: Zawodnicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnicy zawodnicy = db.Zawodnicies.Find(id);
            if (zawodnicy == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pozycji = new SelectList(db.Pozycjes, "Id_Pozycji", "Pozycja", zawodnicy.Id_Pozycji);
            return View(zawodnicy);
        }

        // POST: Zawodnicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Zawodnika,Imię,Nazwisko,Wiek,Id_Pozycji,Ilość_występów,Strzelone_gole,Asysty,Czyste_konta,Zdjęcie")] Zawodnicy zawodnicy, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                ImageFile.SaveAs(physicalPath);
                zawodnicy.Zdjęcie = "images/" + ImageName;

                db.Entry(zawodnicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pozycji = new SelectList(db.Pozycjes, "Id_Pozycji", "Pozycja", zawodnicy.Id_Pozycji);
            return View(zawodnicy);
        }

        // GET: Zawodnicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnicy zawodnicy = db.Zawodnicies.Find(id);
            if (zawodnicy == null)
            {
                return HttpNotFound();
            }
            return View(zawodnicy);
        }

        // POST: Zawodnicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zawodnicy zawodnicy = db.Zawodnicies.Find(id);
            db.Zawodnicies.Remove(zawodnicy);
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
