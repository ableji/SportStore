using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Model.Domain;
using SportStore.Service;
using Newtonsoft.Json;
using SportStore.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SportStore.Web.Infrastructure.Filters;

namespace SportStore.Web.Controllers
{
    
    public class OrderController : AblBaseController
    {
        #region Initial

        private ICityService _cityService;
        private IProvinceService _provianceService;
        private IOrderService _orderService;
        private IProductService _productService;
        private UserManager<AppUser> _userManager;

        public OrderController(ICityService cityService, IProvinceService provinceService,
            IOrderService orderService, UserManager<AppUser> userManager, IProductService productService)
        {
            _cityService = cityService;
            _provianceService = provinceService;
            _orderService = orderService;
            _userManager = userManager;
            _productService = productService;
        }

        #endregion


        public ViewResult Index()
        {
            List<CartItem> cartItems = ReadCartItemsFromCookie();
            return View(cartItems);
        }

        [AjaxOnly]
        public IActionResult RemoveFromCart(int ProductId)
        {
            List<CartItem> cartItems = ReadCartItemsFromCookie();

            if (cartItems.Any(ci => ci.Product.ID == ProductId))
            {
                CartItem cartItem = cartItems.Where(ci => ci.Product.ID == ProductId).FirstOrDefault();
                cartItems.Remove(cartItem);
                SaveCartItemsTOCookie(cartItems);
                return PartialView("_LoadCart", cartItems);

            }

            return Json("Error");

        }

        [AjaxOnly]
        public async Task<JsonResult> AddToCart(int ProductId)
        {
            try
            {

                Product product = await _productService.GetByID(ProductId);
                List<CartItem> cartItems = ReadCartItemsFromCookie();

                if (!cartItems.Any(ci => ci.Product.ID == ProductId))
                {
                    CartItem cartItem = new CartItem { Product = product, Quantity = 1 };
                    cartItems.Add(cartItem);
                }
                else
                {
                    cartItems.Where(ci => ci.Product.ID == ProductId).FirstOrDefault().Quantity++;
                }

                SaveCartItemsTOCookie(cartItems);
                return Json("Success");

            }
            catch (Exception ex)
            {
                //MyLogger.LogException(ex);
                return Json("Error");
            }



        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> CheckOut()
        {
            string userName = User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(userName);
            //user has address
            if (user.CityId != 0) return RedirectToAction("SubmitOrder", "Order");
            //user hasnt address
            else return RedirectToAction("Index", "User");

        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> SubmitOrder()
        {
            try
            {
                Order order = new Order();
                order.CartItems = ReadCartItemsFromCookie();
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                order.UserId = user.Id;
                if (order.CartItems != null)
                {
                    foreach (var item in order.CartItems)
                    {
                        var product = await _productService.GetByID(item.Product.ID);
                        if (product.Quantity < item.Quantity)
                            ModelState.AddModelError("", "Your Request number is more than Inventory");

                        else
                        {
                            product.Quantity -= item.Quantity;
                            await _productService.Update(product);
                        }
                    }

                    if (ModelState.IsValid)
                    {


                        await _orderService.Save(order);
                        EmptyCartItemsIncooki();
                        return RedirectToAction("ThankYou");


                    }

                    else
                    {
                        return View("Index",order.CartItems);
                    }

                }

                else
                {
                    return View("Index",order.CartItems);
                    //TODO: alert cart is empty}
                }

            }


            catch (Exception ex)
            {
                //MyLogger.LogException(ex);
                return RedirectToAction("CheckOut");

            }


        }

        [Authorize(Roles = "User")]
        public ViewResult ThankYou() => View("ThankYou");

        //-----------
        [AjaxOnly]
        public JsonResult GetCitiesByProvianceId(int provianceId)
        {
            try
            {
                var cirties = _cityService.GetCitiesByProvinceID(provianceId);
                return new JsonResult(JsonConvert.SerializeObject(cirties));
            }
            catch (Exception ex)
            {
                //MyLogger.LogException(ex);
                return Json("Error");
            }
        }

        [AjaxOnly]
        public async  Task<IActionResult> GetProviances()
        {
            try
            {
                var a = JsonConvert.SerializeObject( await _provianceService.GetAll());
                return new JsonResult(a);
            }
            catch (Exception ex)
            {
                //MyLogger.LogException(ex);
                return StatusCode(505);
            }
        }







    }
}
