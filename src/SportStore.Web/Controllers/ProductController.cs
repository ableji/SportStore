using Microsoft.AspNetCore.Mvc;
using SportStore.Model.Domain;
using SportStore.Service;
using SportStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Controllers
{
    public  class ProductController : AblBaseController
    {
        #region Initil

        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        #endregion

        public async Task<IActionResult> Index(int page = 1, int ItemsPerPage = 6, int categoryId = 0)

        {
            try
            {
                IEnumerable<Product> products = await _productService.GetAll(categoryId);

                return View(new ProductListViewModel
                {
                    Products = products.OrderBy(p => p.ID).Skip((page - 1) * ItemsPerPage).Take(ItemsPerPage),
                    PagingInfo = new PaginationViewModel
                    {
                        CurrentPage = page,
                        ItemsPerPage = ItemsPerPage,
                        TotalItems = products.Count()
                    },
                    Category = await _productCategoryService.GetByID(categoryId)

                });
            }

            catch (Exception ex)
            {
                //MyLogger.LogException(ex);
                return View("ErrorPage");
            }

        }


        public async Task<IActionResult> Search(string key)
        {
            var res =await _productService.Search(key);
            ViewBag.key = key;
            ViewBag.count = res.Count();
            return View(res);

        }


        public async Task<IActionResult> GetProduct(int id)
        {

            var result = await _productService.GetByID(id);
               
            if (result == null) return NotFound();
            return View("ProductDetail", result);
        }

    }

}

