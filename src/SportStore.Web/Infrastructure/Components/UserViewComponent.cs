using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Infrastructure.Components
{
    public class UserViewComponent : ViewComponent
    {
        private UserManager<AppUser> _userManager;

        public UserViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)

                if(User.IsInRole("Administrator"))

                    return View("_AdminDopDown", User.Identity.Name);
                else

                    return View("_UserDopDown", User.Identity.Name);

            else

            return new HtmlContentViewComponentResult(
                new HtmlString("<li><a href='/Account/Login'><i class='fa fa-sign-in'></i> Login</a></li>" +
                "<li><a href='/Account/Signup'><i class='fa fa-sign-in'></i> SignUp</a></li>"));
                      

        }

    }
}
