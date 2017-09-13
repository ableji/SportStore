using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Service;
using SportStore.Model.Domain;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SportStore.Web.Areas.Admin.Models;

namespace SportStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area(areaName: "Admin")]
    public class AdminController : Controller
    {

        #region Initial

        private IOrderService _orderService;
        private IProductService _productService;
        private IProvinceService _provinceService;
        private ICityService _cityService;
        private IProductCategoryService _productCategoryService;
        private IMessageService _messageService;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(IOrderService orderService, ICityService cityservice,
            IProvinceService provinceService, IProductService productService,
            IProductCategoryService productCategoryService,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager
            , IMessageService messageService)
        {
            _orderService = orderService;
            _provinceService = provinceService;
            _cityService = cityservice;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _userManager = userManager;
            _roleManager = roleManager;
            _messageService = messageService;
        }

        #endregion


        public async  Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                UserCount = _userManager.Users.Count(),
                MessageCount = await _messageService.Count(),
                OrderCount = await _orderService.Count(),
                ProductCount = await _productService.Count(),
                NewMessages = await _messageService.GetSome(4)
            };
            return View(model);
        }

        #region Order

       
        public async Task<IActionResult> LoadPendingOrders()
        {
            try
            {
                IEnumerable<Order> model = await _orderService.GetPendingOrders();
                foreach (var i in model)
                {
                    //i.CityTitle = _cityService.GetCityName(i.CityID);
                    //i.ProvinceTitle = _provinceService.GetProvinceName(i.ProvinceID);
                }
                return PartialView("_LoadOrders", model);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }

        public async Task<IActionResult> PendingOrders() => View(await _orderService.GetPendingOrders());

        public async Task<IActionResult> ShipedOrders() => View(await _orderService.GetAll());

        public async Task<IActionResult> ShipOrder(int OrderId)
        {
            try
            {
                Order order =await _orderService.GetByID(OrderId);
                if (order != null)
                {
                    order.Shiped = true;
                    await _orderService.Update(order);
                    return PartialView("_LoadPendingOrders",await _orderService.GetPendingOrders());
                }

                else
                {
                    return RedirectToAction("PendingOrders");
                }
            }
            catch (Exception ex)
            {
                //log ex
                return RedirectToAction("PendingOrders");
            }
        }

        #endregion

        #region Product

        public async Task<IActionResult> LoadProducts()
        {
            try
            {
               return  PartialView("_LoadProducts", await _productService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
           
        }

        public async Task<ViewResult> Products() => View(await _productService.GetAll());



        public async Task<IActionResult> RemoveProduct(int productID)
        {
            try
            {
                await _productService.DeleteById(productID);
                return RedirectToAction("LoadProducts");
            }

            catch (Exception ex)
            {
                return RedirectToAction("LoadProducts");
            }
        }



        public ViewResult CreateProduct() => View(new Product());

        [HttpPost]
        public async Task<IActionResult> CreateProduct([Bind(include: "ProductName,ProductPrice,ProductDescription,ProductCagegoryID,ImageId,ShortDescription,Quantity")]Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.Save(product);
                return RedirectToAction("Products");
            }

            else
            {
                return View();
            }
        }
        


        public async Task<IActionResult> EditProduct(int id)
        {
            Product product = await _productService.GetByID(id);
            if (product != null)
            {
                return View(product);
            }

            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
             
            product.ID = int.Parse(Request.Form["ID"]);

            if (ModelState.IsValid)
            {
                await _productService.Update(product);
                return RedirectToAction("Products");
            }

            else
            {
                return View();
            }

        }



        #endregion

        #region ProductCategoru
        //Tree + json = Treeson
        private void CreateTreeson(IEnumerable<ProductCategory> categories, ref string json)
        {
            foreach (var item in categories)
            {
                //load childs
                item.Childs = _productCategoryService.LoadChilds(item.ID).First().Childs;

                if (item.Childs.Count == 0) //no child
                {
                    json += (JsonConvert.SerializeObject(new { text = item.Name, id = item.ID }));
                    json += ',';

                }

                else //has child
                {
                    json += "{";
                    json += string.Format("%text%:%{0}%,%id%:%{1}%,%nodes%:[", item.Name, item.ID.ToString());

                    CreateTreeson(item.Childs, ref json);

                    json = json.Remove(json.Length - 1, 1);
                    json += "]},";


                }


            }


        }

        public ViewResult ProductCategories() => View();

        public async Task<string> GetProductCategories()
        {
            IEnumerable<ProductCategory> categories =
               await _productCategoryService.GetForTreeView();


            string strjson = "[";
            CreateTreeson(categories, ref strjson);
            strjson = strjson.Remove(strjson.Length - 1, 1);
            strjson += "]";
            strjson = strjson.Replace('%', '"');

            return strjson;

        }

        public async Task<string> GetProductCategoriess()
        {
            return JsonConvert.SerializeObject(await _productCategoryService.GetAllForDropdown(), Formatting.None,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        }

        public async Task<bool> CreateProductCategory(int paretnID, string name)
        {
            try
            {
                ProductCategory pc = new ProductCategory
                {
                    Name = name,
                    ParentID = paretnID
                };


                await _productCategoryService.Save(pc);
                return true;
            }

            catch (Exception)
            {
                //log it
                return false;

            }

        }

        public async Task<bool> DeleteProductCategory(int id)
        {
            try
            {
                await _productCategoryService.DeleteById(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        
        #endregion

        #region Users


        public IActionResult ListUsers() =>
            PartialView("_LoadUsers", _userManager.Users);

        public IActionResult Users()
        {
            return View( _userManager.Users);
        }

        public async Task<IActionResult> RemoveUser(string UserId)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Users");
            }

            else
                return View();
        }


        #endregion




    }
}
