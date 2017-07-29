using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pack4Travel.Models;

namespace Pack4Travel.Controllers
{
    public class equipementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: equipements
        public ActionResult Index()
        {
            var equipements = db.equipements.Include(e => e.userInfo);
            return View(equipements.ToList());
        }

        // GET: equipements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipements equipements = db.equipements.Find(id);
            if (equipements == null)
            {
                return HttpNotFound();
            }
            return View(equipements);
        }

        // GET: equipements/Create
        public ActionResult Create()
        {
            ViewBag.idOwner = new SelectList(db.userInfo, "idUser", "userName");
            return View();
        }

        // POST: equipements/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEquipement,equipementName,idGroup,idOwner,privateStatus")] equipements equipements)
        {
            if (ModelState.IsValid)
            {
                db.equipements.Add(equipements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idOwner = new SelectList(db.userInfo, "idUser", "userName", equipements.idOwner);
            return View(equipements);
        }

        // GET: equipements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipements equipements = db.equipements.Find(id);
            if (equipements == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOwner = new SelectList(db.userInfo, "idUser", "userName", equipements.idOwner);
            return View(equipements);
        }

        // POST: equipements/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEquipement,equipementName,idGroup,idOwner,privateStatus,five_stars,four_stars,three_stars,two_stars,one_star")] equipements equipements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOwner = new SelectList(db.userInfo, "idUser", "userName", equipements.idOwner);
            return View(equipements);
        }

        // GET: equipements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipements equipements = db.equipements.Find(id);
            if (equipements == null)
            {
                return HttpNotFound();
            }
            return View(equipements);
        }

        // POST: equipements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipements equipements = db.equipements.Find(id);
            db.equipements.Remove(equipements);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Rate(int id, int rate)
        {
            equipements equipements = db.equipements.Find(id);
            switch(rate)
            {
                case 5: equipements.five_stars++; break;
                case 4: equipements.four_stars++; break;
                case 3: equipements.three_stars++; break;
                case 2: equipements.two_stars++; break;
                case 1: equipements.one_star++; break;
            }
            
            db.SaveChanges();
            return RedirectToAction($"Details/{id}", "equipements");
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
