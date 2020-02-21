using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline_JesusGarceran_MiW.Models;

namespace ShopOnline_JesusGarceran_MiW.Controllers
{
    public class ProfileController : Controller
    {
        
        private ShopModelContainer db = new ShopModelContainer();

        // GET: Profile

        [Authorize]
        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Authorize]
        // GET: Profile/Create
        public ActionResult Create()
        {
            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();
            else
                c.Email = User.Identity.Name;


            return View(c);
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,FullName,Phone,Address,Email,City,Country,PostalCode,CreditCard")] Client client)
        {
            if (ModelState.IsValid)
            {
                var res = (from cli in db.Clients
                           where cli.Email.Equals(User.Identity.Name)
                           select cli);

                if (res.Count() == 0)
                {
                    client.Email = User.Identity.Name;
                    db.Clients.Add(client);
                }
                else
                {
                    Client c = res.First();
                    c.Username = client.Username;
                    c.FullName = client.FullName;
                    c.Phone = client.Phone;
                    c.Address = client.Address;
                    c.City = client.City;
                    c.Country = client.Country;
                    c.PostalCode = client.PostalCode;
                    c.CreditCard = client.CreditCard;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                return RedirectToAction("Index", "Home", null);
            }

            return View(client);
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
