using ShopOnline_JesusGarceran_MiW.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline_JesusGarceran_MiW.Controllers
{
    public class CartController : Controller
    {
  
        private ShopModelContainer db = new ShopModelContainer();
        private Order order = null;
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(Cart cart, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (cart.Exists(p => p.Id == product.Id))
            {
                Product exist = cart.Find(p => p.Id == product.Id);
                if (product.Stock > exist.Stock)
                {
                    exist.Stock = exist.Stock + 1;
                }
                else
                {
                    return RedirectToAction("Show");
                }
            }
            else
            {
                product.Stock = 1;
                cart.Add(product);
            }

            return RedirectToAction("Show");
        }


        public ActionResult Delete(Cart cart, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            cart.RemoveAll(p => p.Id == id);
            return RedirectToAction("Show");
        }

        public ActionResult Buy(Cart cart)
        {
            order = new Order();
            order.Date = DateTime.Now;

            var res = (from cli in db.Clients
                       where cli.Email.Equals(User.Identity.Name)
                       select cli);

            if (res.Count() != 0)
            {
                order.Client = res.First();
            }
            else
            {
                return RedirectToAction("Create", "Profile");
            }

            double precioTotal = 0;
            foreach (Product p in cart)
            {
                Product product = db.Products.Find(p.Id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                product.Stock = product.Stock - p.Stock;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                precioTotal = precioTotal + (product.Prize * p.Stock);

                Purchase purchase = new Purchase();
                purchase.Order = order;
                purchase.Product = product;
                purchase.Quantity = p.Stock;

                db.Purchases.Add(purchase);
            }

            order.TotalPrize = precioTotal;
            db.Orders.Add(order);

            db.SaveChanges();
        
            ViewBag.Pedido = order;
            return View("ShowPedido", cart);
        }

        public ActionResult ShowPedido(Cart cart)
        {
            return View(cart);
        }

        public ActionResult Show(Cart cart)
        {
            return View(cart);
        }

    }
}