using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SportStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Infrastructure.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        #region Initial
        private IProductCategoryService _productCategoryService;

        public CategoriesViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int categoryId,int itemsPerPage)
        {
            ViewBag.ItemsPerPage = itemsPerPage;
            if (categoryId == 0)
            {
                var categories = await _productCategoryService.GetRootCategories();
                return View("CategoriesList",categories);
            }

            else
            {
                var categories = _productCategoryService.LoadChilds(categoryId);
                return View("CategoriesList",categories.FirstOrDefault().Childs);
            }


        }
    }
}
