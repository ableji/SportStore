using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Infrastructure.Components
{
    public class CartViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cartitems = 
              (Request.Cookies["_cart"] != null) ?
              JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["_cart"]) : new List<CartItem>();

            return new HtmlContentViewComponentResult(
                new HtmlString($"<li><a href='/Order'><i class='fa fa-shopping-cart'></i> Cart <span class='badge'>{cartitems.Count}</span></a></li>"));
        }
    }
}
