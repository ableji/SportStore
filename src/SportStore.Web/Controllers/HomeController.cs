using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Model.Domain;
using SportStore.Service;

namespace SportStore.Web.Controllers
{
    public class HomeController:AblBaseController
    {
        #region Initial
        private IMessageService _messageService;
        public HomeController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        #endregion

        public IActionResult Error()
        {
            return View();
        }
        


        public ViewResult SendMessage()
        {
            string userName = User.Identity.Name;
            return View(new Message { Name = userName });
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([Bind(include: "Name,Subject,body")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.SendDate = DateTime.Now;
                await _messageService.Save(message);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }
        }

    }
}
