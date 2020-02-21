using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline_JesusGarceran_MiW.Models.Binders
{
    public class CartModelBinder : IModelBinder
    {
        public static string key_carrito = "KEY_123_CARRITO";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart c = (Cart)controllerContext.HttpContext.Session[key_carrito];

            if (c == null)
            {
                c = new Cart();
                controllerContext.HttpContext.Session[key_carrito] = c;
            }

            return c;
        }
    }
}