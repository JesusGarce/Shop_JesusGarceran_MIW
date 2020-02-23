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
    public class OrdersController : Controller
    {
        private ShopModelContainer db = new ShopModelContainer();

        // GET: Orders
        public ActionResult Index()
        {

            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            Client c = new Client();

            if (res.Count() != 0)
                c = res.First();

            return View(db.Orders.Where(o => o.Client.Id == c.Id).ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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
