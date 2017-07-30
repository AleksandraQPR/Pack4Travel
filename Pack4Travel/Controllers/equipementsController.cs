using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pack4Travel.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pack4Travel.Controllers
{
    public class equipementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: equipements
        public ActionResult Index()
        {
            var equipements = db.equipements;
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
            return View();
        }

        // POST: equipements/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEquipement,equipementName,idGroup,Id,privateStatus")] equipements equipements)
        {
            if (ModelState.IsValid)
            {
                equipements.Id = User.Identity.GetUserId();
                db.equipements.Add(equipements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            if (equipements.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(equipements);
        }

        // POST: equipements/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEquipement,equipementName,idGroup,privateStatus")] equipements equipements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipements).State = EntityState.Modified;
                db.SaveChanges();
                equipements tmp = db.equipements.Find(equipements.idEquipement);
                tmp.Id = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

        public ActionResult Fork(int id)
        {
            equipements equipements = db.equipements.Find(id);
            equipements newequipements = new equipements();

            newequipements = equipements;

            //deep copy of items
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(stream, equipements);
            //    stream.Position = 0;

            //    newequipements = (equipements)formatter.Deserialize(stream);
            //}

            //remove rating
            newequipements.five_stars = 0;
            newequipements.four_stars = 0;
            newequipements.three_stars = 0;
            newequipements.two_stars = 0;
            newequipements.one_star = 0;

            //change ownership
            newequipements.Id = User.Identity.GetUserId();

            db.equipements.Add(newequipements);
            db.SaveChanges();

            return RedirectToAction($"Edit/{newequipements.idEquipement}", "equipements");
        }

        public ActionResult Private()
        {
            var equipements = db.equipements;
            return View(equipements.ToList());
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
