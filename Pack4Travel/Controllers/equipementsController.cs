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
using Rotativa;

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
            var AllItemsFromDB = (from item in db.items select item).ToList();
            var equipentList = new equipements();
            equipentList.items = new List<items>() { new items() };

            foreach (var item in AllItemsFromDB)
            {
                equipentList.items.Add(item);
            }
            return View(equipentList);
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
                var idFromDB = equipements.idEquipement;

                foreach(var i in db.items)
                {
                    equipements.items.Add(i);
                }

                db.SaveChanges();
                return RedirectToAction($"Edit/{equipements.idEquipement}", "equipements");
            }

            return View(equipements);
        }


        [HttpPost]
        public ActionResult UseShippingAddress(int number, string secondField)
        {
            //write your logic here to save the file on a disc            
            return Json("1");
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
            if (!equipements.items.Any())
            {
                equipements.items.Add(new items());
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

        public ActionResult AddNewItemToDb()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewItemToDb(items item)
        {
            db.items.Add(item);
            db.SaveChanges();

            var currentEquipementId = TempData["equipementId"];
            var currentEquipement = db.equipements.Find(currentEquipementId);
            db.equipements.Find(currentEquipementId).items.Add(item);

            db.SaveChanges();
            return PartialView("_ItemsOfEquipementListPartial", currentEquipement);

           // return Json("Dziękujemy, element został zapisany w bazie");
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

        [HttpPost]
        public ActionResult SelectItemsFromDB(items item, FormCollection form)
        {
            string selectedItemId = form["ItemsList"].ToString();
            items selectedItem = db.items.Find(Int32.Parse(selectedItemId));
            var currentEquipementId = TempData["equipementId"];
            db.equipements.Find(currentEquipementId).items.Add(selectedItem);
            db.SaveChanges();
            return RedirectToAction($"Edit/{currentEquipementId}", "equipements");
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

        public ActionResult DeleteItemFromOwnerList(int idOfEqupementList, int idOfItem)
        {
            equipements equipements = db.equipements.Find(idOfEqupementList);
            var actualListOfItems = equipements.items.ToList();
            var newListOfItems = from item in actualListOfItems where item.idItem != idOfItem select item;
            equipements.items = null;
            db.SaveChanges();
            equipements.items = new List<items>();
            foreach (var item in newListOfItems)
                equipements.items.Add(item);
            db.SaveChanges();
            return RedirectToAction($"Edit/{equipements.idEquipement}", "equipements");
        }

        public ActionResult GetAllItemsFromDB()
        {
            var allItemsFromDB = db.items.OrderBy(i=>i.itemName).ToList();
            return PartialView("_ListOfItemsPartial", allItemsFromDB);
        }



        public ActionResult Rate(int id, int rate)
        {
            equipements equipements = db.equipements.Find(id);
            switch (rate)
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

            foreach (var i in equipements.items)
            {
                newequipements.items.Add(i);
            }
            db.equipements.Add(newequipements);

            db.SaveChanges();
            return RedirectToAction($"Edit/{newequipements.idEquipement}", "equipements");
        }

        public ActionResult PrintDetails(int id)
        {
            return new ActionAsPdf($"Details/{id}") { FileName = "Pack4Travel.pdf" };
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
