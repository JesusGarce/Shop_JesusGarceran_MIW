using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline_JesusGarceran_MiW.Models;

namespace ShopOnline_JesusGarceran_MiW.Controllers
{
    public class AdminPanelController : Controller
    {
        private ShopModelContainer db = new ShopModelContainer();

        // GET: AdminPanel
        public ActionResult Index()
        {
            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();

            if (c.Rol.Equals("Admin"))
                return View(db.Products.ToList());
            else
                return RedirectToAction("Index", "Home");
        }

        // GET: AdminPanel/Create
        public ActionResult Create()
        {
            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();

            if (c.Rol.Equals("Admin"))
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        // POST: AdminPanel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Image,Stock")] Product product)
        {
            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();

            if (!c.Rol.Equals("Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: AdminPanel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();

            if (!c.Rol.Equals("Admin"))
                return RedirectToAction("Index", "Home");

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminPanel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Image,Stock")] Product product)
        {

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: AdminPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();

            if (!c.Rol.Equals("Admin"))
                return RedirectToAction("Index", "Home");

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
