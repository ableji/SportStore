using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SportStore.Model.Domain;
using Microsoft.AspNetCore.Authorization;
using SportStore.Service;

namespace SportStore.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : AblBaseController
    {
        #region Initial
        private UserManager<AppUser> _userManager;
        private ICityService _cityService;
        private IOrderService _orderService;
        //private SignInManager<AppUser> _signInManager;
        //private RoleManager<IdentityRole> _roleManager;


        public UserController(UserManager<AppUser> userMGR, ICityService cityService, IOrderService orderService)
        {
            _userManager = userMGR;
            _cityService = cityService;
            _orderService = orderService;
            //_signInManager = signInMGR;
            //_roleManager = roleManager;
        }

        #endregion


        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            //user.Orders=_orderService.GetUserOrders(user.Id).ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfo(AppUser model)
        {

            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);


                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.CityId = model.CityId;
                user.RestOfAddress = model.RestOfAddress;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) return Json("Successfully Updated ");
                else
                {
                    string errors = "";
                    foreach (var item in result.Errors)
                    {
                        errors+= item.Code;
                    }

                    return Json(errors);
                }


            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string CurrentPassword, string NewPassword)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
            if (result.Succeeded) return Json("Password Updated");
            else
            {
                string res="";
                foreach (var item in result.Errors)
                {
                     res += item.Code;

                }
                return Json(res);
            }
        }
    }
}
