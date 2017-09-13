using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SportStore.Model.Domain;
using SportStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Initial
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userMGR, SignInManager<AppUser> signInMGR, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userMGR;
            _signInManager = signInMGR;
            _roleManager = roleManager;
        }

        #endregion



        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await
                    _userManager.FindByNameAsync(loginModel.Username);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();


                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        IList<string> roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Administrator"))
                            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        else
                            return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }

                ModelState.AddModelError("", "Invalid Username or Password");
                return View(loginModel);
            }

            ModelState.AddModelError("", "Invalid Username or Password");
            return View(loginModel);
        }


        [AllowAnonymous]
        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var res = await _userManager.AddToRoleAsync(user, "User");
                    if (res.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true, null);
                        return RedirectToAction("Index", "User");
                    }

                    else
                    {
                        ModelState.AddModelError("", "error");
                        return View();
                    }
                }

                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Code);
                    }
                    
                    return View(model);
                }
            }


            return View(model);


        }


        [Authorize]
        public async Task<RedirectResult> SignOut(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }


        public ViewResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}

