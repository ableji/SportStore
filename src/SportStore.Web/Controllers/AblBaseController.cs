using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportStore.Model.Domain;
using Newtonsoft.Json;

namespace SportStore.Web.Controllers
{
    public class AblBaseController : Controller
    {

        #region Utility

        protected void SaveCartItemsTOCookie(IEnumerable<CartItem> items)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            string jsonCart = JsonConvert.SerializeObject(items);
            Response.Cookies.Append("_cart", jsonCart, cookieOptions);
        }

        protected List<CartItem> ReadCartItemsFromCookie() =>
           (Request.Cookies["_cart"] != null) ?
            JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["_cart"]) : new List<CartItem>();


        protected void EmptyCartItemsIncooki()
        {
            Response.Cookies.Delete("_cart");
        }

        #endregion
    }
}
